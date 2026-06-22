using Business.Common;
using MediatR;

namespace Business.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Result<Guid>>;
}
