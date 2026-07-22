using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(Email email);
        Task<bool> ExistsByEmailAsync(Email email);
    }
}
