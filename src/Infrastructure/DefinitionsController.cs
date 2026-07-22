using Business.InstrumentDefinitions.GetInstrumentDefinitions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure
{
    [ApiController]
    [Route("api/definitions")]
    public sealed class DefinitionsController : ControllerBase
    {
        private readonly ISender sender;

        public DefinitionsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("instruments")]
        public async Task<IResult> GetInstrumentDefinitions(
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetInstrumentDefinitionsQuery(),
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
    }
}
