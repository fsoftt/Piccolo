using Domain.Common;
using MediatR;

namespace Business.Users.Commands.LoginUser
{
    public record LoginUserCommand(
        string Email, 
        string Password) 
        : IRequest<Result<string>>;
}
