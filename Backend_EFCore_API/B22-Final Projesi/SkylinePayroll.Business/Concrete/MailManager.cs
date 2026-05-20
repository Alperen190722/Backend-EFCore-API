using SkylinePayroll.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Concrete
{
    public class MailManager : IMailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            Debug.WriteLine($"[EMAIL SENT] To: {to} | Subject: {subject}");
            Debug.WriteLine($"Content: {body}");
            Debug.WriteLine("-----------------------------------------------");
        }
    }
}
