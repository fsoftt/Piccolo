using Business.Messaging;
using Domain.Organizations.Events;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Outbox
{
    public sealed class InvitationCreatedHandler
    {
        private readonly IEmailSender emailSender;
        private readonly ILogger<InvitationCreatedHandler> logger;

        public InvitationCreatedHandler(IEmailSender emailSender, ILogger<InvitationCreatedHandler> logger)
        {
            this.emailSender = emailSender;
            this.logger = logger;
        }

        public async Task HandleAsync(InvitationCreatedDomainEvent evt, CancellationToken cancellationToken)
        {
            var subject = "You're invited to join an organization";
            var body = $"You have been invited to organization {evt.OrganizationId}. Use this token to accept the invitation: {evt.Token}";

            logger.LogInformation("Processing InvitationCreatedDomainEvent for invitation {InvitationId} to {Email}", evt.InvitationId, evt.Email);

            await emailSender.SendAsync(evt.Email, subject, body, cancellationToken);
        }
    }
}
