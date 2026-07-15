using Business.Abstractions.Authentication;
using Domain.Common;
using MediatR;

namespace Business.Users.Queries.GetCurrentUser
{
    public sealed class GetCurrentUserHandler
        : IRequestHandler<
            GetCurrentUserQuery,
            Result<GetCurrentUserResponse>>
    {
        private readonly ICurrentUser currentUser;

        public GetCurrentUserHandler(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        public Task<Result<GetCurrentUserResponse>> Handle(
            GetCurrentUserQuery request, 
            CancellationToken cancellationToken)
        {
            if (currentUser.UserId is null) {
                return Task.FromResult(Result<GetCurrentUserResponse>.Failure(
                    new Error("Users.NotAuthenticated", "User is not authenticated.")));
            }

            var response = new GetCurrentUserResponse(
                currentUser.UserId.Value,
                currentUser.Email ?? string.Empty);

            return Task.FromResult(
                Result<GetCurrentUserResponse>.Success(response));
        }
    }
}
