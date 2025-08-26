using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RalliesUK.Application.Interfaces;
using RalliesUK.Domain.Entities;
using RalliesUK.Infrastructure.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RalliesUK.Infrastructure.Servicecs
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly SymmetricSecurityKey _key;
        private readonly IUserRoleService _userRoleService;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings, IUserRoleService userRoleService)
        {
            _jwtSettings = jwtSettings.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            _userRoleService = userRoleService;
        }

        public async Task<string> CreateTokenAsync(IUserToken user, DateTime expiration)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            if (expiration <= DateTime.UtcNow)
            {
                throw new ArgumentException("Expiration time must be in the future.", nameof(expiration));
            }

            var claims = CreateClaims(user);
            await AddRolesToClaimsAsync(user, claims);

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = creds,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static List<Claim> CreateClaims(IUserToken user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("User email cannot be null or empty.", nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ArgumentException("User name cannot be null or empty.", nameof(user));
            }

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.GivenName, user.UserName)
            };
            return claims;
        }

        private async Task AddRolesToClaimsAsync(IUserToken user, List<Claim> claims)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("User email cannot be null or empty.", nameof(user));
            }

            var roles = await _userRoleService.GetRolesAsync(user.Email!);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }
    }
}
