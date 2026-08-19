using Domain.Common;
using Domain.Instruments;
using MediatR;

namespace Business.Organizations.Instruments.UpdateInstrument
{
    public sealed record UpdateOrganizationInstrumentCommand(
        Guid OrganizationId,
        Guid InstrumentId,
        string Name,
        InstrumentFamily Family,
        Guid? InstrumentDefinitionId)
    : IRequest<Result>;
}
