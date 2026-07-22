using Business.Authentication;
using System.Security.Claims;

namespace API.Authentication
{
    public sealed class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated =>
            httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        public Guid? UserId 
        {
            get
            {
                string? idAsString = httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst(
                        ClaimTypes.NameIdentifier)?
                    .Value;

                return Guid.TryParse(
                    idAsString,
                    out var userId)
                    ? userId
                    : null;
            }
        }

        public string? Email
        {
            get
            {
                return httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirst(
                        ClaimTypes.Email)?
                    .Value;
            }
        }
    }
}
