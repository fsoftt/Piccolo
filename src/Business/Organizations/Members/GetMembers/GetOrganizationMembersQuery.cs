using Domain.Common;
using MediatR;

namespace Business.Organizations.Members.GetMembers
{
    public sealed record GetOrganizationMembersQuery(
        Guid OrganizationId)
        : IRequest<Result<IReadOnlyCollection<OrganizationMemberResponse>>>;
}
