using Business.Abstractions.Repositories;
using Domain.Users;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task<bool> ExistsByEmailAsync(Email email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
