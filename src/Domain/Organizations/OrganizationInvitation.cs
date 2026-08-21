using Domain.Common;
using Domain.Users.ValueObjects;

namespace Domain.Organizations
{
    public sealed class OrganizationInvitation : AggregateRoot
    {
        private OrganizationInvitation()
        {
        }

        private OrganizationInvitation(
            Guid id,
            Guid organizationId,
            Email email,
            string token,
            InvitationStatus status,
            DateTime createdAt,
            DateTime expiresAt)
        {
            Id = id;
            OrganizationId = organizationId;
            Email = email;
            Token = token;
            Status = status;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public Guid OrganizationId { get; private set; }

        public Email Email { get; private set; }

        public string Token { get; private set; }

        public InvitationStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        public static OrganizationInvitation Create(
            Guid organizationId,
            Email email,
            string hashedToken,
            DateTime expiresAt)
        {
            return new OrganizationInvitation(
                Guid.NewGuid(),
                organizationId,
                email,
                hashedToken,
                InvitationStatus.Pending,
                DateTime.UtcNow,
                expiresAt);
        }

        public void AddCreatedEvent(string unhashedToken)
        {
            var evt = new Events.InvitationCreatedDomainEvent(
                Id,
                OrganizationId,
                Email.Value,
                unhashedToken,
                ExpiresAt,
                CreatedAt);

            AddDomainEvent(evt);
        }
    }
}
