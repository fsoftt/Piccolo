using Domain.Common;
using MediatR;

namespace Business.Users.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Result<Guid>>;
}
