using Domain.Common;
using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public sealed class User : AggregateRoot
    {
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        private User() 
        { 
        }

        private User(
            Guid id,
            Email email,
            PasswordHash passwordHash) 
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static Result<User> Create(
            Email email,
            PasswordHash passwordHash)
        {
            var user = new User(
                Guid.NewGuid(),
                email,
                passwordHash);

            return Result<User>.Success(user);
        }
    }
}
