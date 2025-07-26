using Microsoft.AspNetCore.Identity;
using RalliesUK.Application.Requests.Auth;

namespace RalliesUK.Application.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterRequest registerRequest);
    }
}