using System.Security.Cryptography;
using Business.Authentication;

namespace Infrastructure.Authentication
{
    public class InvitationTokenGenerator : IInvitationTokenGenerator
    {
        public string GenerateToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToHexString(tokenBytes);
        }
    }
}
