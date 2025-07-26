using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RalliesUK.Infrastructure.Configuration;

namespace RalliesUK.Infrastructure.Extensions.Startup
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.Configure<JwtSettings>(configurationManager.GetRequiredSection("Jwt"));
            return services;
        }
    }
}