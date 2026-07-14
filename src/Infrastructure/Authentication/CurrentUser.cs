using Business.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

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
