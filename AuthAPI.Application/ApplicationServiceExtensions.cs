using AuthAPI.Application.Common;
using AuthAPI.Application.Services;
using AuthAPI.Domain.Interfaces;
using AuthAPI.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthAPI.Application
{  
    /// <summary>
    /// Extension methods for configuring application layer services
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Adds application layer services to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The application configuration</param>
        /// <returns>The service collection for method chaining</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register application services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

            // Configure authentication settings
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));

            return services;
        }
    }
}