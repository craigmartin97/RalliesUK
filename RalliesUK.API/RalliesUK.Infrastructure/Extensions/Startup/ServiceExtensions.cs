using Microsoft.Extensions.DependencyInjection;
using RalliesUK.Application.Interfaces;
using RalliesUK.Application.Services;
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
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            return services;
        }

        private static IServiceCollection AddInfrastructureServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, JwtTokenService>();
            return services;
        }
    }
}