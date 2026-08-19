using Domain.Organizations;

namespace Business.Organizations.Members.AddMember
{
    public sealed record AddMemberResponse(
        Guid OrganizationId,
        Guid UserId,
        OrganizationRole Role,
        MemberStatus Status,
        DateTime JoinedAt);
}
