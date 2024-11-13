using apihealthcareconnect.ViewModel.Requests.Login;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

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
            string sender = Environment.GetEnvironmentVariable("MAIL_SENDER");
            string password = Environment.GetEnvironmentVariable("MAIL_PASSWORD");
            string server = Environment.GetEnvironmentVariable("MAIL_SERVER");

            if (string.IsNullOrEmpty(sender))
            {
                sender = smtpDataCovered["EmailSender"];
            }
            if (string.IsNullOrEmpty(password))
            {
                password = smtpDataCovered["EmailPassword"];
            }
            if (string.IsNullOrEmpty(server))
            {
                server = smtpDataCovered["Server"];
            }

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(sender));
            email.To.Add(MailboxAddress.Parse(request.to));
            email.Subject = request.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.body };


            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(server, 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("resend", password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
