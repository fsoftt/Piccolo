using Domain.Common;
using MediatR;

namespace Business.Organizations.CreateOrganization
{
    public sealed record CreateOrganizationCommand(
        string Name)
    : IRequest<Result<CreateOrganizationResponse>>;
}
