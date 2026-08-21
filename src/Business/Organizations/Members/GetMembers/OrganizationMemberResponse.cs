using Domain.Organizations;

namespace Business.Organizations.Members.GetMembers
{
    public sealed record OrganizationMemberResponse(
        Guid UserId,
        OrganizationRole Role,
        MemberStatus Status,
        DateTime JoinedAt);
}
