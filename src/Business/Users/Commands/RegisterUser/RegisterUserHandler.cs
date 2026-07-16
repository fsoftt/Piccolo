using Business.Abstractions.Authentication;
using Business.Abstractions.Persistence;
using Business.Abstractions.Repositories;
using Domain.Common;
using Domain.Users;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;
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
            var userEmail = Email.Create(request.Email);
            if (userEmail.IsFailure)
            {
                return Result<Guid>.Failure(
                    new Error("Email.InvalidFormat", "The provided email format is invalid."));
            }

            bool exists = await userRepository.ExistsByEmailAsync(userEmail.Value!);
            if (exists)
            {
                return Result<Guid>.Failure(UserErrors.EmailAlreadyExists);
            }

            string hashedPassword = passwordHasher.Hash(request.Password);
            Result<PasswordHash> passwordHashResult = PasswordHash.Create(hashedPassword);
            if (passwordHashResult.IsFailure)
            {
                return Result<Guid>.Failure(passwordHashResult.Error);
            }

            Result<Email> emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result<Guid>.Failure(emailResult.Error);
            }

            Result<User> userResult = User.Create(emailResult.Value!, passwordHashResult.Value!);
            if (userResult.IsFailure)
            {
                return Result<Guid>.Failure(userResult.Error);
            }

            var user = userResult.Value!;

            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);
        }
    }
}
