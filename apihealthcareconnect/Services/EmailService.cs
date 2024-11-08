using apihealthcareconnect.ViewModel.Requests.Login;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Configuration;
using System.Xml.Linq;

namespace apihealthcareconnect.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task sendEmail(SendEmailViewModel request)
        {
            var smtpDataCovered = _configuration?.GetSection("SmtpData");
            var sender = smtpDataCovered["EmailSender"];
            var password = smtpDataCovered["EmailPassword"];
            var server = smtpDataCovered["Server"];

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(sender));
            email.To.Add(MailboxAddress.Parse(request.to));
            email.Subject = request.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.body };


            

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(server, 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(sender, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
