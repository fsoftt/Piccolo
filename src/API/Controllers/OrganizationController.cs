using API.Contracts.Organizations;
using Business.Organizations.CreateOrganization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/organizations")]
    [Authorize]
    public sealed class OrganizationsController : ControllerBase
    {
        private readonly ISender sender;

        public OrganizationsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost]
        public async Task<IResult> Create(
            CreateOrganizationRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateOrganizationCommand(
                request.Name);

            var result = await sender.Send(
                command,
                cancellationToken);
            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.Error.Code,
                    Message = result.Error.Description
                });
            }

            return Results.Created(
                $"/api/organizations/{result.Value!.Id}",
                result.Value);
        }
    }
}
