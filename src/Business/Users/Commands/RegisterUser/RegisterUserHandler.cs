using Business.Abstractions.Authentication;
using Business.Abstractions.Persistence;
using Business.Abstractions.Repositories;
using Domain.Users;
using MediatR;

namespace Business.Users.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;

        public RegisterUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            this.unitOfWork = unitOfWork;
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
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
