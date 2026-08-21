using Domain.Common;

namespace Domain.Organizations.Events
{
    public sealed class InvitationCreatedDomainEvent : IDomainEvent
    {
        public Guid InvitationId { get; }

        public Guid OrganizationId { get; }

        public string Email { get; }

        public string Token { get; }

        public DateTime ExpiresAt { get; }

        public DateTime CreatedAt { get; }

        public InvitationCreatedDomainEvent(Guid invitationId, Guid organizationId, string email, string token, DateTime expiresAt, DateTime createdAt)
        {
            InvitationId = invitationId;
            OrganizationId = organizationId;
            Email = email;
            Token = token;
            ExpiresAt = expiresAt;
            CreatedAt = createdAt;
        }
    }
}
