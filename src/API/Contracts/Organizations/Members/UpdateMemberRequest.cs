using Domain.Organizations;

namespace API.Contracts.Organizations.Members
{
    public sealed record UpdateMemberRequest(
        MemberStatus Status);
}
