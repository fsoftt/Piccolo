using Business.Abstractions.Authentication;
using Business.Abstractions.Persistence;
using Business.Abstractions.Repositories;
using Business.Common;
using Business.Common.Errors;
using Domain.Users;
using MediatR;

namespace Business.Users.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
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

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool exists = await userRepository.ExistsByEmailAsync(request.Email);
            if (exists)
            {
                return Result<Guid>.Failure(UserErrors.EmailAlreadyExists);
            }

            string hash = passwordHasher.Hash(request.Password);
            var user = new User(request.Email, hash);
            
            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);
        }
    }
}
