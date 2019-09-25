using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.Options;

namespace WebCoreAppFramework.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IHostingEnvironment _env;
        private readonly ILogger logger;

        public EmailService(IOptions<EmailConfiguration> emailConfiguration,
            IHostingEnvironment env, ILogger<EmailService> Logger)
        {
            _emailConfiguration = emailConfiguration.Value;
            _env = env;
            logger = Logger;
            logger.LogInformation("EmailService Initialized");
        }

        public async Task<List<EmailMessage>> ReceiveEmailAsync(int maxCount = 10)
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

        public async Task SendEmailAsync(string address, string subject, string body)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.ToAddresses.Add(new EmailAddress { Address = address });
            emailMessage.Subject = subject;
            emailMessage.Content = body;
            await this.SendEmailAsync(emailMessage);

        }
        public async Task SendEmailAsync(EmailMessage emailMessage)
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

                if (emailMessage.BccAddresses.Any())
                {

                    message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                }
                else
                {
                    message.From.Add(new MailboxAddress(_emailConfiguration.DefaultEmailName, _emailConfiguration.DefaultEmailAddress));
                }
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
            
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
