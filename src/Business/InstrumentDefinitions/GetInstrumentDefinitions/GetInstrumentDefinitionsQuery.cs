using Domain.Common;
using MediatR;

namespace Business.InstrumentDefinitions.GetInstrumentDefinitions
{
    public sealed record GetInstrumentDefinitionsQuery
        : IRequest<Result<IReadOnlyList<InstrumentDefinitionResponse>>>;
}
