using Business.Common;
using MediatR;

namespace Business.Users.Queries.GetCurrentUser
{
    public sealed record GetCurrentUserQuery
        : IRequest<Result<GetCurrentUserResponse>>;
}
