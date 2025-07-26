using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RalliesUK.Application.Entities;
using RalliesUK.Infrastructure.Repositories.Identity;

namespace RalliesUK.Infrastructure.Extensions.Startup
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 12;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}