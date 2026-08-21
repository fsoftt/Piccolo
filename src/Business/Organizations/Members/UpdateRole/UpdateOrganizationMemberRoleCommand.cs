using Domain.Common;
using MediatR;

namespace Business.Organizations.Members.UpdateRole
{
    public sealed record UpdateOrganizationMemberRoleCommand(
        Guid OrganizationId,
        Guid UserId,
        Domain.Organizations.OrganizationRole Role)
        : IRequest<Result>;
}
