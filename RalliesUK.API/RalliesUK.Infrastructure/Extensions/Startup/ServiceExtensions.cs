using Microsoft.Extensions.DependencyInjection;
using RalliesUK.Application.Interfaces;
using RalliesUK.Infrastructure.Servicecs;

namespace RalliesUK.Infrastructure.Extensions.Startup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            AddApplicationServices(services);
            AddInfrastructureServices(services);
            return services;
        }

        private static IServiceCollection AddApplicationServices(IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddInfrastructureServices(IServiceCollection services)
        {
            // Add tokens service
            services.AddScoped<ITokenService, JwtTokenService>();

            // Add user management services
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            return services;
        }
    }
}