using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
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

            builder.MapPost("api/auth/login", async (LoginRequest loginRequest, IUserAuthenticationService userAuthenticationService, ITokenService tokenService, ILogger logger) =>
            {
                if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                {
                    return Results.BadRequest("Email and password are required.");
                }

                try
                {
                    var authenticated = await userAuthenticationService.LoginAsync(loginRequest);
                    if (!authenticated)
                    {
                        return Results.Unauthorized();
                    }
                }
                catch(Exception ex) 
                {
                    logger.LogError(ex, "Error authenticating user {Email}", loginRequest.Email);
                    return Results.Problem(title: "An internal error occurred while processing the login request.", statusCode: StatusCodes.Status500InternalServerError);
                }

                var userToken = new ApplicationUser
                {
                    UserName = loginRequest.Email,
                    Email = loginRequest.Email
                };

                string token;
                try
                {
                    token = await tokenService.CreateTokenAsync(userToken, DateTime.UtcNow.AddDays(7));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error creating token for user {Email}", loginRequest.Email);
                    return Results.Problem(title: "An internal error occurred while issuing the token.", statusCode: StatusCodes.Status500InternalServerError);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    return Results.Ok(token);
                }
                else
                {
                    // Token creation returned an empty value — treat as server error rather than a credential problem
                    logger.LogWarning("Token service returned empty token for user {Email}", loginRequest.Email);
                    return Results.Problem(title: "Failed to issue authentication token.", statusCode: StatusCodes.Status500InternalServerError);
                }
            });
        }

        private static ApplicationUser CreateApplicationUser(string? email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();
            return new ApplicationUser
            {
                UserName = normalized,
                Email = normalized
            };
        }
    }
}