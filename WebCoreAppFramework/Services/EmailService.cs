using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.Options;

namespace WebCoreAppFramework.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IHostingEnvironment _env;
        private readonly ILogger logger;

        public EmailService(IEmailConfiguration emailConfiguration,
            IHostingEnvironment env, ILogger<EmailService> Logger)
        {
            _emailConfiguration = emailConfiguration;
            _env = env;
            logger = Logger;
            logger.LogInformation("EmailService Initialized");
        }

        public async Task<List<EmailMessage>> ReceiveAsync(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await emailClient.ConnectAsync(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                await emailClient.AuthenticateAsync(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }

                return emails;
            }
        }

        public async Task SendAsync(EmailMessage emailMessage)
        {
            try
            {
                var message = new MimeMessage();
                if (emailMessage.ToAddresses.Any())
                {
                    message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                }
                else
                {
                    throw new InvalidOperationException("Can't Send messages without any destination address");
                }

                if (emailMessage.CcAddresses.Any()) message.Cc.AddRange(emailMessage.CcAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                if (emailMessage.BccAddresses.Any()) message.Bcc.AddRange(emailMessage.BccAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                message.Subject = emailMessage.Subject;
                //We will say we are sending HTML. But there are options for plaintext etc. 
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailMessage.Content
                };

                //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                using (var emailClient = new SmtpClient())
                {
                    emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //The last parameter here is to use SSL (Which you should!)
                    await emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                    //Remove any OAuth functionality as we won't be using it. 
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    await emailClient.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                    await emailClient.SendAsync(message);

                    await emailClient.DisconnectAsync(true);
                }

            }
            catch (System.Exception ex)
            {
                logger.LogError(ex,ex.Message);
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
