using Domain.Common;

namespace Domain.Organizations
{
    public sealed class OrganizationMemberPermission : Entity
    {
        private OrganizationMemberPermission()
        {
        }

        private OrganizationMemberPermission(
            OrganizationPermission permission)
        {
            Id = Guid.NewGuid();
            Permission = permission;
        }

        public OrganizationPermission Permission { get; private set; }

        internal static OrganizationMemberPermission Create(
            OrganizationPermission permission)
        {
            return new OrganizationMemberPermission(permission);
        }
    }
}
