using API.Contracts.Organizations;
using API.Extensions;
using Business.Organizations.AddInstrument;
using Business.Organizations.ConfigureInstruments;
using Business.Organizations.CreateOrganization;
using Business.Organizations.GetInstruments;
using Business.Organizations.GetMyOrganizations;
using Business.Organizations.RemoveInstrument;
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

        [HttpGet]
        public async Task<IResult> GetMyOrganizations(
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetMyOrganizationsQuery(),
                cancellationToken);
            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.Error.Code,
                    result.Error.Description
                });
            }

            return Results.Ok(result.Value);
        }

        [HttpPut("{organizationId:guid}/instruments")]
        public async Task<IActionResult> ConfigureInstruments(
            Guid organizationId,
            ConfigureOrganizationInstrumentsRequest request,
            CancellationToken cancellationToken)
        {
            var command = new ConfigureOrganizationInstrumentsCommand(
                organizationId,
                request.Instruments);

            var result = await sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return result.ToProblem(this);
            }

            return NoContent();
        }

        [HttpGet("{organizationId:guid}/instruments")]
        public async Task<IResult> GetInstruments(
            Guid organizationId,
            CancellationToken cancellationToken)
        {
            var query = new GetOrganizationInstrumentsQuery(
                organizationId);

            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.Error.Code,
                    Message = result.Error.Description
                });
            }

            return Results.Ok(result.Value);
        }

        [HttpPost("{organizationId:guid}/instruments")]
        public async Task<IResult> AddInstrument(
            Guid organizationId,
            AddOrganizationInstrumentRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddOrganizationInstrumentCommand(
                organizationId,
                request.Name,
                request.Family,
                request.InstrumentDefinitionId);

            var result = await sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.Error.Code,
                    Message = result.Error.Description
                });
            }

            return Results.Created(
                $"/api/organizations/{organizationId}/instruments/{result.Value}",
                result.Value);
        }

        [HttpDelete("{organizationId:guid}/instruments/{instrumentId:guid}")]
        public async Task<IResult> RemoveInstrument(
            Guid organizationId,
            Guid instrumentId,
            CancellationToken cancellationToken)
        {
            var command = new RemoveOrganizationInstrumentCommand(
                organizationId,
                instrumentId);

            var result = await sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.Error.Code,
                    Message = result.Error.Description
                });
            }

            return Results.NoContent();
        }
    }
}
