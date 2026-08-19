using Domain.Common;
using MediatR;

namespace Business.Organizations.Instruments.RemoveInstrument
{
    public sealed record RemoveOrganizationInstrumentCommand(
        Guid OrganizationId,
        Guid InstrumentId)
    : IRequest<Result>;
}
