using Microsoft.AspNetCore.Identity;
using RalliesUK.Domain.Entities;

namespace RalliesUK.Application.Entities
{
    public class ApplicationUser : IdentityUser, IUserToken
    {
        string? IUserToken.Email => base.Email;
        string? IUserToken.UserName => base.UserName;
    }
}
