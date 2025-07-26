using RalliesUK.Infrastructure.Configuration;

namespace RalliesUK.Infrastructure.Helpers
{
    public static class JwtSettingsCreator
    {
        public static JwtSettings CreateTokenSettings(string? signingKey, string? issuer, string? audience)
        {
            if (string.IsNullOrWhiteSpace(signingKey) || signingKey.Length < 64)
            {
                throw new InvalidOperationException("JWT signing key is too short. It must be at least 64 characters for HMAC SHA-512.");
            }

            if (string.IsNullOrWhiteSpace(issuer))
            {
                throw new InvalidOperationException("JWT issuer is not configured.");
            }

            if (string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException("JWT audience is not configured.");
            }

            return new JwtSettings
            {
                SigningKey = signingKey,
                Issuer = issuer,
                Audience = audience,
            };
        }
    }
}
