using Domain.Common;

namespace Domain.Users.Errors
{
    public static class UserErrors
    {
        public static readonly Error EmailAlreadyExists = 
            new Error(
                "Users.EmailAlreadyExists", 
                "Email already exists");
        public static readonly Error InvalidCredentials = 
            new Error(
                "Users.InvalidCredentials", 
                "Invalid credentials");
        public static readonly Error InvalidPasswordHash =
            new Error(
                "Users.InvalidPasswordHash",
                "Invalid password hash");

        public static readonly Error EmailNotFound =
            new Error(
                "Users.EmailNotFound",
                "User with provided email was not found. Please create the user first.");
    }
}
