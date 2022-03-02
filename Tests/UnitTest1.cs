using MailKit.Net.Smtp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimeKit;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void EmailSendTest()
        {
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("Site Bot", "kazyten@ya.ru"));
            emailMessage.To.Add(new MailboxAddress("User", "ktsin@tut.by"));
            emailMessage.Subject = "subject";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "htmlMessage"
            };

            using var client = new SmtpClient();
            client.ConnectAsync("smtp.yandex.ru", 465, true).Wait();
            client.AuthenticateAsync("", "").Wait();
            client.SendAsync(emailMessage).Wait();
            
 
            client.DisconnectAsync(true);
        }
    }
}