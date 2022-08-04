using Core.Models;
using Core.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        //private readonly MailMessage _emailSettings;

        //public EmailService(IConfiguration config, MailMessage emailSettings)
        //{
        //    _config = config;
        //    _emailSettings = emailSettings;
        //}

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmailAsync(Email emailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailRequest.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        //public void SendEmailAsync(Email emailRequest)
        //{
        //    MailMessage mail = new MailMessage();
        //    mail.IsBodyHtml = true;
        //    if (!string.IsNullOrEmpty(_emailSettings.Sender))
        //    {
        //        Subject = _emailSettings.Sender;
        //    }

        //    mail.Subject = new MailAddress(_emailSettings.From, Subject);

        //}
    }
}
