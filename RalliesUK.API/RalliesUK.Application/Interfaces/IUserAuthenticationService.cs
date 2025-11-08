using Microsoft.AspNetCore.Identity;
using RalliesUK.Application.Requests.Auth;

namespace RalliesUK.Application.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<bool> LoginAsync(LoginRequest request);

        Task<IdentityResult> RegisterUserAsync(RegisterRequest request);
    }
}