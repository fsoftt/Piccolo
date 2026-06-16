using Business.Abstractions.Authentication;
using Business.Abstractions.Repositories;
using Domain.Users;
using MediatR;

namespace Business.Users.Commands.LoginUser
{
    public class LoginUserHandler
        : IRequestHandler<LoginUserCommand, string>
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

        public async Task<string> Handle(
            LoginUserCommand request, 
            CancellationToken cancellationToken)
        {
            User? user = await users.GetByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("Invalid email or password.");
            }

            bool valid = hasher.Verify(request.Password, user.PasswordHash!);
            if (!valid)
            {
                throw new Exception("Invalid email or password.");
            }

            return jwtProvider.Generate(user);
        }
    }
}
