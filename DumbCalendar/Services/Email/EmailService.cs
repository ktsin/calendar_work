using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;


namespace DumbCalendar.Services.Email
{
    public class EmailService : IEmailSender
    {
        private readonly MessageSenderOption _options;

        public EmailService(IOptions<MessageSenderOption> options)
        {
            _options = options?.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.Run(() =>
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Site Bot", _options.EmailSenderLogin));
                emailMessage.To.Add(new MailboxAddress("User", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlMessage
                };

                using var client = new SmtpClient();
                client.ConnectAsync("smtp.yandex.ru", 465, true).Wait();
                client.AuthenticateAsync(_options.EmailSenderLogin, _options.EmailSenderPassword).Wait();
                client.SendAsync(emailMessage).Wait();

                client.DisconnectAsync(true).Wait();
            });
        }
    }
}