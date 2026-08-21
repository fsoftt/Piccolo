using Domain.Common;
using Domain.Organizations;
using MediatR;

namespace Business.Organizations.Members.UpdateMember
{
    public sealed record UpdateOrganizationMemberCommand(
        Guid OrganizationId,
        Guid UserId,
        MemberStatus Status)
        : IRequest<Result>;
}
