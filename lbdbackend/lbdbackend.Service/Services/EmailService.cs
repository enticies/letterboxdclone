using lbdbackend.Service.DTOs.AccountDTOs;
using lbdbackend.Service.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.Services {
    public class EmailService : IEmailService {
        public void Register(RegisterDTO registerDTO, string link) {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("LbdClone", "lbdemailsender@gmail.com"));
            message.To.Add(new MailboxAddress(registerDTO.Username, registerDTO.Email));
            message.Subject = "Confirmation Email";

            string emailbody = "<a href='[URL]'>Confirmation Link</a>".Replace("[URL]", link);
            using var smtp = new SmtpClient();
            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("lbdemailsender@gmail.com", "zzgfgwsfhuxfdavn");
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
