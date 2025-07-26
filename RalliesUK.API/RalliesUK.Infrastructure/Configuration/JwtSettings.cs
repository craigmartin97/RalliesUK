namespace RalliesUK.Infrastructure.Configuration
{
    public record JwtSettings
    {
        public required string SigningKey { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }

        public static JwtSettings Create(string signingKey, string issuer, string audience)
        {
            return new JwtSettings
            {
                SigningKey = signingKey,
                Issuer = issuer,
                Audience = audience
            };
        }
    }
}