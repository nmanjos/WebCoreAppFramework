using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.Service
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
