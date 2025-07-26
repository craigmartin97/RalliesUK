using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RalliesUK.Infrastructure.Repositories.Identity;

namespace RalliesUK.Infrastructure.Extensions.Startup
{
    public static class EFDatabaseContextExtensions
    {
        public static IServiceCollection AddEFDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string 'DefaultConnection' is not configured.");
            }

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}