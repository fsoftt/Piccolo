using Domain.Common;

namespace Domain.Users.Errors
{
    public static class AuthenticationErrors
    {
        public static readonly Error Unauthorized = 
            new Error(
                "Authentication.Unauthorized", 
                "Unauthorized");
    }
}
