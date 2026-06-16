using Domain.Users;

namespace Business.Abstractions.Authentication
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
