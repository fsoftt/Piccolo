using API.Contracts.Organizations;
using API.Contracts.Organizations.Instruments;
using API.Contracts.Organizations.Members;
using API.Extensions;
using Business.Organizations.CreateOrganization;
using Business.Organizations.GetMyOrganizations;
using Business.Organizations.Instruments.AddInstrument;
using Business.Organizations.Instruments.ConfigureInstruments;
using Business.Organizations.Instruments.GetInstruments;
using Business.Organizations.Instruments.RemoveInstrument;
using Business.Organizations.Instruments.UpdateInstrument;
using Business.Organizations.Members.AddMember;
using Business.Organizations.Members.GetMembers;
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

        [HttpPut("{organizationId:guid}/instruments/{instrumentId:guid}")]
        public async Task<IResult> UpdateInstrument(
            Guid organizationId,
            Guid instrumentId,
            UpdateOrganizationInstrumentRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateOrganizationInstrumentCommand(
                organizationId,
                instrumentId,
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

            return Results.NoContent();
        }

        [HttpPost("{organizationId:guid}/members")]
        public async Task<IResult> AddMember(
            Guid organizationId,
            AddMemberRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddMemberCommand(
                organizationId,
                request.UserId);

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
               $"/api/organizations/{organizationId}/members/{result.Value!.UserId}",
               result.Value);
        }

        [HttpGet("{organizationId:guid}/members")]
        public async Task<IResult> GetMembers(
            Guid organizationId,
            CancellationToken cancellationToken)
        {
            var query = new GetOrganizationMembersQuery(
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

        [HttpPatch("{organizationId:guid}/members/{userId:guid}/status")]
        public async Task<IResult> UpdateMember(
            Guid organizationId,
            Guid userId,
            UpdateMemberRequest request,
            CancellationToken cancellationToken)
        {
            var command = new Business.Organizations.Members.UpdateMember.UpdateOrganizationMemberCommand(
                organizationId,
                userId,
                request.Status);

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

        [HttpPatch("{organizationId:guid}/members/{userId:guid}/role")]
        public async Task<IResult> UpdateMemberRole(
            Guid organizationId,
            Guid userId,
            UpdateMemberRoleRequest request,
            CancellationToken cancellationToken)
        {
            var command = new Business.Organizations.Members.UpdateRole.UpdateOrganizationMemberRoleCommand(
                organizationId,
                userId,
                request.Role);

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
