using Domain.Common;

namespace Domain.Users.Errors
{
    public static class PasswordHashErrors
    {
        public static readonly Error Empty =
            new(
                "PasswordHash.Empty",
                "Password hash cannot be empty.");
    }
}
