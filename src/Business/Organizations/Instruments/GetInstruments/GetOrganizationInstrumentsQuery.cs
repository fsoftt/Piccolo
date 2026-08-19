using Domain.Common;
using MediatR;

namespace Business.Organizations.Instruments.GetInstruments
{
    public sealed record GetOrganizationInstrumentsQuery(
        Guid OrganizationId)
        : IRequest<Result<IReadOnlyCollection<OrganizationInstrumentResponse>>>;
}
