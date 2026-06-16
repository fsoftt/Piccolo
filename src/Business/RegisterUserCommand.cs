using MediatR;

namespace Business
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;
}
