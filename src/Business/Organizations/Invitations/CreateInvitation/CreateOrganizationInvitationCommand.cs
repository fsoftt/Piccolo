using Domain.Common;
using MediatR;

namespace Business.Organizations.Invitations.CreateInvitation
{
    public sealed record CreateOrganizationInvitationCommand(
        Guid OrganizationId,
        string Email)
        : IRequest<Result<CreateOrganizationInvitationResponse>>;
}
