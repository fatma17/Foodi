using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Foodi.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _MailServer;
        private readonly int _MailPort;
        private readonly string _FromEmail;
        private readonly string _Password;

        public EmailSender(string Server, int Port, string Email, string Pass)
        {
            _MailServer = Server;
            _MailPort = Port;
            _FromEmail = Email;
            _Password = Pass;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(_FromEmail);

            using (var client = new SmtpClient(_MailServer, _MailPort))
            {
                client.Credentials = new NetworkCredential(_FromEmail, _Password);
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
