using Domain.Common;

namespace Domain.Organizations
{
    public sealed class OrganizationMember : Entity
    {
        private readonly List<OrganizationMemberPermission> permissions = [];

        private OrganizationMember()
        {
        }

        private OrganizationMember(
            Guid organizationId,
            Guid userId,
            OrganizationRole role)
        {
            Id = Guid.NewGuid();
            OrganizationId = organizationId;
            UserId = userId;
            Role = role;
            Status = MemberStatus.Active;
            JoinedAt = DateTime.UtcNow;
        }

        public Guid OrganizationId { get; private set; }

        public Guid UserId { get; private set; }

        public OrganizationRole Role { get; private set; }

        public MemberStatus Status { get; private set; }

        public DateTime JoinedAt { get; private set; }

        public bool HasPermission(OrganizationPermission permission)
        {
            return permissions.Any(x => x.Permission == permission);
        }

        public IReadOnlyCollection<OrganizationMemberPermission> Permissions =>
            permissions.AsReadOnly();

        internal static OrganizationMember CreateOwner(
            Guid organizationId,
            Guid userId)
        {
            var owner = new OrganizationMember(
                organizationId,
                userId,
                OrganizationRole.Owner);

            foreach (var permission in OrganizationPermissions.Owner)
            {
                owner.permissions.Add(
                    OrganizationMemberPermission.Create(permission));
            }

            return owner;
        }

        internal static OrganizationMember CreateMember(
            Guid organizationId,
            Guid userId)
        {
            return new OrganizationMember(
                organizationId,
                userId,
                OrganizationRole.Member);
        }
    }
}
