using Domain.Common;
using MediatR;

namespace Business.Organizations.RemoveInstrument
{
    public sealed record RemoveOrganizationInstrumentCommand(
        Guid OrganizationId,
        Guid InstrumentId)
    : IRequest<Result>;
}
