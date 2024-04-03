using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmail(string senderName, string subject, string senderEmail, string body)
        {
            try
            {
                var recipientEmail = _emailSettings.RecipientEmail;
                var password = _emailSettings.Password;
                var smtpHost = _emailSettings.Host;
                var port = _emailSettings.Port;

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(senderName, senderEmail));
                email.To.Add(new MailboxAddress("", recipientEmail));
                email.Subject = subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpHost, port, true);
                    await client.AuthenticateAsync(recipientEmail, password);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending email: {Message}", ex.Message);

            }
        }
    }

}
