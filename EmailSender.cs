using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace DemoProject
{
    public class EmailSender : IEmailSender
    {
        public class ConsoleEmailSenderMock : IEmailSender
        {
            private readonly ILogger<ConsoleEmailSenderMock> logger;

            public ConsoleEmailSenderMock(ILogger<ConsoleEmailSenderMock> logger){
                this.logger = logger;
            }

            public Task SendEmailAsync(string email, string subject, string htmlMessage)
            {
                logger.LogDebug($@"email: {email}, subject: {subject}, message: {HttpUtility.HtmlDecode(htmlMessage)}");
                
                return Task.CompletedTask;
            }
        }

        // Our private configuration variables
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        private readonly IConfiguration configuration;
        private readonly ILogger<EmailSender> logger;

        // Get our parameterized configuration
        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            this.configuration = configuration;
            this.logger = logger;

            this.host = configuration.GetSection(nameof(EmailSender)).GetValue<string>(nameof(host));
            this.port = configuration.GetSection(nameof(EmailSender)).GetValue<int>(nameof(port));
            this.enableSSL = configuration.GetSection(nameof(EmailSender)).GetValue<bool>(nameof(enableSSL));
            this.userName = configuration.GetSection(nameof(EmailSender)).GetValue<string>(nameof(userName));
            this.password = configuration.GetSection(nameof(EmailSender)).GetValue<string>(nameof(password));
        }

        // Use our configuration to send the email by using SmtpClient
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };
            logger.LogDebug($@"About to send mail from {userName} to {email}.");
            logger.LogDebug($@"email: {email}, subject: {subject}, message: {htmlMessage}");
            
            return client.SendMailAsync(
                new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }
    }
}