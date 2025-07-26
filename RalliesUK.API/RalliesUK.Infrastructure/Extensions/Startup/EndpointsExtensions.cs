using Microsoft.AspNetCore.Builder;
using RalliesUK.Infrastructure.Extensions.Endpoints;

namespace RalliesUK.Infrastructure.Extensions.Startup
{
    public static class EndpointsExtensions
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
            // Rallies
            app.MapGetRallyEndpoints();

            // Authentication
            app.MapAuthEndpoints();
        }
    }
}
