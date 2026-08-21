using Domain.Common;
using MediatR;

namespace Business.Organizations.Members.RemoveMember
{
    public sealed record RemoveOrganizationMemberCommand(
        Guid OrganizationId,
        Guid UserId)
        : IRequest<Result>;
}
