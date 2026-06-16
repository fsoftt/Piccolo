using MediatR;

namespace Business.Users
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;
}
