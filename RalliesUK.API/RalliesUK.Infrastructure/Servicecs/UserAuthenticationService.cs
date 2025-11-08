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

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
            };
            return await _userManager.CreateAsync(user, request.Password);
        }

        public async Task<bool> LoginAsync(LoginRequest request)
        {
            ArgumentNullException.ThrowIfNull(request.Email);
            ArgumentException.ThrowIfNullOrWhiteSpace(request.Password);

            var user = await FindByEmailAsync(request.Email).ConfigureAwait(false);
            if (user is null)
            {
                throw new UserNotFoundException("", request.Email);
            }

            return await _userManager.CheckPasswordAsync(user, request.Password);
        }

        private Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email);
            return _userManager.FindByEmailAsync(email);
        }
    }
}
