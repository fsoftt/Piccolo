using Domain.Organizations;

namespace API.Contracts.Organizations.Members
{
    public sealed record UpdateMemberRoleRequest(
        OrganizationRole Role);
}
