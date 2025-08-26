using Microsoft.AspNetCore.Identity;
using RalliesUK.Application.Entities;
using RalliesUK.Application.Interfaces;
using RalliesUK.Application.Requests.Auth;
using RalliesUK.Domain.Exceptions;

namespace RalliesUK.Infrastructure.Servicecs
{
    public sealed class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
            };
            return await _userManager.CreateAsync(user, registerRequest.Password);
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            var user = await FindByEmailAsync(email).ConfigureAwait(false);
            if (user is null)
            {
                throw new UserNotFoundException("", email);
            }

            return await _userManager.CheckPasswordAsync(user, password);
        }

        private Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email);
            return _userManager.FindByEmailAsync(email);
        }
    }
}
