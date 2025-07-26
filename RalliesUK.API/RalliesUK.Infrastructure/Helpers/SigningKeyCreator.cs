using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RalliesUK.Infrastructure.Helpers
{
    internal static class SigningKeyCreator
    {
        public static SecurityKey CreateSigningKey(string? signingKey)
        {
            ArgumentException.ThrowIfNullOrEmpty(signingKey, nameof(signingKey));
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }
    }
}
