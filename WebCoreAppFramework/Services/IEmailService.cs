using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string address, string subject, string body);
        Task SendEmailAsync(EmailMessage emailMessage);
        Task<List<EmailMessage>> ReceiveEmailAsync(int maxCount = 10);
    }
}
