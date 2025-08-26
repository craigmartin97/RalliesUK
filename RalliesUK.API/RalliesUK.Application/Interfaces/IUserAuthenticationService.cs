using Microsoft.AspNetCore.Identity;
using RalliesUK.Application.Requests.Auth;

namespace RalliesUK.Application.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<bool> LoginAsync(string email, string password);

        Task<IdentityResult> RegisterUserAsync(RegisterRequest registerRequest);
    }
}