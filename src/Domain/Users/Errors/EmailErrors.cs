using Domain.Common;

namespace Domain.Users.Errors
{
    public static class EmailErrors
    {
        public static readonly Error Empty = 
            new Error(
                "Email.Empty", 
                "Email cannot be empty.");

        public static readonly Error InvalidFormat =
            new Error(
                "Email.InvalidFormat",
                "Email format is invalid.");
    }
}
