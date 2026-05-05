using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Scan.BLL.Services.EmailServices
{
    public class SmtpEmailService(IConfiguration _config) : IEmailService
    {
        public async Task SendAsync(string to, string subject, string body)
        {
            var smtpSection = _config.GetSection("Smtp");

            using var client = new SmtpClient(smtpSection["Host"])
            {
                Port = int.Parse(smtpSection["Port"]),
                Credentials = new NetworkCredential(
               smtpSection["Username"],
               smtpSection["Password"]),
                EnableSsl = true,
                UseDefaultCredentials = false
            };

            var mail = new MailMessage
            {
                From = new MailAddress(smtpSection["Username"], "CarScan AI"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mail.To.Add(to);

            await client.SendMailAsync(mail);

        }
    }
}
