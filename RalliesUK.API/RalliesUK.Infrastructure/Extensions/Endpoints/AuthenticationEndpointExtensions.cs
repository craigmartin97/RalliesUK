using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RalliesUK.Application.Entities;
using RalliesUK.Application.Interfaces;
using RalliesUK.Application.Requests.Auth;

namespace RalliesUK.Infrastructure.Extensions.Endpoints
{
    internal static class AuthenticationEndpointExtensions
    {
        internal static void MapAuthEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("api/auth/register", async (RegisterRequest registerRequest, IUserRegistrationService userRegistrationService) =>
            {
                var res = await userRegistrationService.RegisterUserAsync(registerRequest);
                if(res.Succeeded)
                {
                    return Results.Ok("User registered successfully");
                }
                else
                {
                    var errors = string.Join(", ", res.Errors.Select(e => e.Description));
                    return Results.BadRequest($"User registration failed: {errors}");
                }
            });

            builder.MapPost("api/auth/login", (LoginRequest loginRequest, ITokenService tokenService) =>
            {
                ApplicationUser userToken = new()
                {
                    Email = loginRequest.Email,
                    UserName = loginRequest.Email,
                };
                string token = tokenService.CreateToken(userToken, DateTime.UtcNow.AddDays(7));
                if (!string.IsNullOrEmpty(token))
                {
                    return Results.Ok("User logged in successfully");
                }
                else
                {
                    return Results.BadRequest("Invalid login attempt");
                }
            });
        }
    }
}