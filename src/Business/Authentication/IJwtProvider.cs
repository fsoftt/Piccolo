using Domain.Users;

namespace Business.Authentication
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
