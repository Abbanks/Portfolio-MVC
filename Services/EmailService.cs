using Portfolio.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace Portfolio.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmail(string senderName, string subject, string senderEmail, string body)
        {
             
                var recipientEmail = _emailSettings.RecipientEmail;
                var password = _emailSettings.Password;
                var smtpHost = _emailSettings.Host;
                var port = _emailSettings.Port;
               
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(senderName, senderEmail));  
                email.To.Add(new MailboxAddress("",recipientEmail));
                email.Subject = subject;
                email.Body = new TextPart("plain") { Text = body };
               


                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpHost, port, true);  
                    await client.AuthenticateAsync(recipientEmail, password); 
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                 
                }

          
        }

 
    }
}
