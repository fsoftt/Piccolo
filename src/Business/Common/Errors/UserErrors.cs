namespace Business.Common.Errors
{
    public static class UserErrors
    {
        public static readonly Error EmailAlreadyExists = new Error("Users.EmailAlreadyExists", "Email already exists");
        public static readonly Error InvalidCredentials = new Error("Users.InvalidCredentials", "Invalid credentials");
    }
}
