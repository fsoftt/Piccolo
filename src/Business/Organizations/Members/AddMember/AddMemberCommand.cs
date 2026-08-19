using Domain.Common;
using MediatR;

namespace Business.Organizations.Members.AddMember
{
    public sealed record AddMemberCommand(
        Guid OrganizationId,
        Guid UserId)
    : IRequest<Result<AddMemberResponse>>;
}
