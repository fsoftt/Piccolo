using Business.Abstractions.Authentication;
using Business.Abstractions.Repositories;
using Domain.User;
using MediatR;

namespace Business
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool exists = await userRepository.ExistsByEmailAsync(request.Email);
            if (exists)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            string hash = passwordHasher.Hash(request.Password);
            var user = new User(request.Email, hash);
            
            await userRepository.AddAsync(user);
            
            return user.Id;
        }
    }
}
