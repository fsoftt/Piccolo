using API.Contracts.Organizations.Invitations;
using Business.Organizations.Invitations.AcceptInvitation;
using Business.Organizations.Invitations.CreateInvitation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public sealed class InvitationsController : ControllerBase
    {
        private readonly ISender sender;

        public InvitationsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost("{token}/accept")]
        [Authorize]
        public async Task<IResult> AcceptInvitation(string token, CancellationToken cancellationToken)
        {
            var command = new AcceptInvitationCommand(token);

            var result = await sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return Results.BadRequest(new { result.Error.Code, Message = result.Error.Description });
            }

            return Results.NoContent();
        }
    }
}
