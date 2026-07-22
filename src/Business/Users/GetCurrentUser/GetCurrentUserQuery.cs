using Domain.Common;
using MediatR;

namespace Business.Users.GetCurrentUser
{
    public sealed record GetCurrentUserQuery
        : IRequest<Result<GetCurrentUserResponse>>;
}
