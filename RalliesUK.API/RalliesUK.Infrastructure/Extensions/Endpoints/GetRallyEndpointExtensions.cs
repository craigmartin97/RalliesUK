using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace RalliesUK.Infrastructure.Extensions.Endpoints
{
    internal static class GetRallyEndpointExtensions
    {
        internal static void MapGetRallyEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("api/rallies", () =>
            {
                return Results.Ok("Got all the rallies!");
            }).RequireAuthorization();
        }
    }
}
