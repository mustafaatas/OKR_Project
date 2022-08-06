using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }

        public void SendEmailAsync(Email emailRequest)
        {
            throw new NotImplementedException();
        }

        //public void SendEmailAsync(Email emailRequest)
        //{
        //    MailMessage mail = new MailMessage();
        //    mail.IsBodyHtml = true;
        //    if (!string.IsNullOrEmpty(_emailSettings.Sender))
        //    {
        //        title = _emailSettings.Sender;
        //    }

        //    mail.Subject = new MailAddress(_emailSettings.From, title);

        //    foreach (var address in mailadress.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        mail.To.Add(address);
        //    }

        //    mail.BodyEncoding = Encoding.UTF8;
        //    mail.Subject = subject;
        //    mail.IsBodyHtml = true;
        //    mail.Body = template;

        //    MailKit.Net.Smtp.SmtpClient client = new SmtpClient()
        //    {
        //        Port = _emailSettings.Port,
        //        Host = _emailSettings.Host,
        //        EnableSsl = _emailSettings.EnableSsl,
        //        Credentials = new NetworkCredential(_emailSettings.Address, _emailSettings.Password)
        //    };

        //    client.Send(mail);
        //}
    }
}
