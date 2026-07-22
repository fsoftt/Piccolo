using Business.Abstractions.Repositories;
using Business.Authentication;
using Domain.Common;
using Domain.Users;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using MediatR;

namespace Business.Users.Commands.LoginUser
{
    public class LoginUserHandler
        : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUserRepository users;
        private readonly IPasswordHasher hasher;
        private readonly IJwtProvider jwtProvider;

        public LoginUserHandler(
            IUserRepository users, 
            IPasswordHasher hasher, 
            IJwtProvider jwtProvider)
        {
            this.users = users;
            this.hasher = hasher;
            this.jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(
            LoginUserCommand request, 
            CancellationToken cancellationToken)
        {
            var userEmail = Email.Create(request.Email);
            if (userEmail.IsFailure)
            {
                return Result<string>.Failure(
                    new Error("Email.InvalidFormat", "The provided email format is invalid."));
            }

            User? user = await users.GetByEmailAsync(userEmail.Value!);
            if (user is null)
            {
                return Result<string>.Failure(UserErrors.InvalidCredentials);
            }

            bool valid = hasher.Verify(request.Password, user.PasswordHash);
            if (!valid)
            {
                return Result<string>.Failure(UserErrors.InvalidCredentials);
            }

            string token = jwtProvider.Generate(user);

            return Result<string>.Success(token);
        }
    }
}
