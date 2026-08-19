using Domain.Common;
using Domain.Instruments;
using MediatR;

namespace Business.Organizations.Instruments.AddInstrument
{
    public sealed record AddOrganizationInstrumentCommand(
        Guid OrganizationId,
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId)
        : IRequest<Result<Guid>>;
}
