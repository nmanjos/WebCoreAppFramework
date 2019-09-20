using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage emailMessage);
        Task<List<EmailMessage>> ReceiveAsync(int maxCount = 10);
    }
}
