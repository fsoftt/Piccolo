using Business.Messaging;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging
{
    public sealed class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            this.logger = logger;
        }

        public Task SendAsync(string email, string subject, string body, CancellationToken cancellationToken)
        {
            logger.LogInformation("[EmailSender] Sent email to {Email} Subject: {Subject} Body: {Body}", email, subject, body);
            return Task.CompletedTask;
        }
    }
}
