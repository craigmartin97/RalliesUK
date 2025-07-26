using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RalliesUK.Infrastructure.Helpers;

namespace RalliesUK.Infrastructure.Extensions.Startup
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddApiAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7125",
                    ValidAudience = "https://localhost:7125",
                    IssuerSigningKey = SigningKeyCreator.CreateSigningKey("xyRECp2bBwt$^Hj^^*MN%mD41w7agbgKkuYF1*&exr7PNa2vVMr6Rt%FfEEt2D3^"),
                };
            });
            return services;
        }
    }
}