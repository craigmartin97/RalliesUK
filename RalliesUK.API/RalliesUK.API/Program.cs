using RalliesUK.Infrastructure.Extensions.Startup;

namespace RalliesUK.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Change type of configuration to ConfigurationManager for improved performance
            ConfigurationManager configuration = builder.Configuration;

            // Create the jwt settings
            builder.Services.AddConfiguration(configuration);

            // Add entity framework context
            builder.Services.AddEFDatabaseContext(configuration);

            builder.Services.AddIdentityServices();

            builder.Services.AddApiAuthentication();
            builder.Services.AddAuthorization();

            builder.Services.AddServices();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // Add all required endpoints
            app.MapAllEndpoints();

            app.Run();
        }
    }
}