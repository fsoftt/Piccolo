using Domain.Common;
using MediatR;

namespace Business.Organizations.Instruments.ConfigureInstruments
{
    public sealed record ConfigureOrganizationInstrumentsCommand(
        Guid OrganizationId,
        IReadOnlyCollection<OrganizationInstrumentRequest> Instruments)
        : IRequest<Result>;
}
