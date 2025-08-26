using Microsoft.AspNetCore.Identity;
using RalliesUK.Application.Entities;
using RalliesUK.Application.Interfaces;
using RalliesUK.Domain.Exceptions;

namespace RalliesUK.Infrastructure.Servicecs
{
    public sealed class UserRoleService : IUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync("e8f14d79-0e4f-4731-91bd-00d8f7a919d1");
            if (user == null)
            {
                throw new UserNotFoundException("User not found", userId);
            }
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}