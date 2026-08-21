using Domain.Common;
using MediatR;

namespace Business.Organizations.Invitations.AcceptInvitation
{
    public sealed record AcceptInvitationCommand(
        string Token)
        : IRequest<Result>;
}
