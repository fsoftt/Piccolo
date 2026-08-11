using Domain.Common;
using MediatR;

namespace Business.Organizations.GetInstruments
{
    public sealed record GetOrganizationInstrumentsQuery(
        Guid OrganizationId)
        : IRequest<Result<IReadOnlyCollection<OrganizationInstrumentResponse>>>;
}
