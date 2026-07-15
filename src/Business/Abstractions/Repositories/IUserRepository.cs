using Domain.Users;
using Domain.Users.ValueObjects;

namespace Business.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(Email email);
        Task<bool> ExistsByEmailAsync(Email email);
    }
}
