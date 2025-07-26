using Microsoft.AspNetCore.Identity;
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

        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        }

        public string CreateToken(IUserToken user, DateTime expiration)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            if(string.IsNullOrWhiteSpace(user.Email))
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

            // NEED TO DO THIS HERE
            //var roles = await _userManager.GetRolesAsync(user);
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration, // DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
