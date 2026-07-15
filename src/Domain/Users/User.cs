using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public string? PasswordHash { get; private set; }

        private User(
            Guid id,
            Email email,
            string passwordHash) 
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }

        public User(Email email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
