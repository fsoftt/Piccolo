using Domain.Common;
using MediatR;

namespace Business.Organizations.GetMyOrganizations
{
    public sealed record GetMyOrganizationsQuery()
        : IRequest<Result<IReadOnlyList<OrganizationSummaryResponse>>>;
}
