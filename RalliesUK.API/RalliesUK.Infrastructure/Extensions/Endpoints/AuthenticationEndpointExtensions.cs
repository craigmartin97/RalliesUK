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
            builder.MapPost("api/auth/register", async (RegisterRequest registerRequest, IUserAuthenticationService userAuthenticationService) =>
            {
                var res = await userAuthenticationService.RegisterUserAsync(registerRequest);
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

            builder.MapPost("api/auth/login", async (LoginRequest loginRequest, IUserAuthenticationService userAuthenticationService, ITokenService tokenService) =>
            {
                bool authenticated = await userAuthenticationService.LoginAsync(loginRequest.Email, loginRequest.Password);
                if(!authenticated)
                {
                    return Results.Unauthorized();
                }

                // Successful login, find the full user object
                string token = await tokenService.CreateTokenAsync(userToken, DateTime.UtcNow.AddDays(7));
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