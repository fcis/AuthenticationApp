using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Enums;
using AuthAPI.Domain.Settings;
using AuthAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthAPI.Infrastructure.Identity
{
    /// <summary>
    /// Extension methods for configuring identity services
    /// </summary>
    public static class IdentityServiceExtensions
    {
        /// <summary>
        /// Adds identity services to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The application configuration</param>
        /// <returns>The service collection for method chaining</returns>
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configure Auth settings from appsettings
            var authSettingsSection = configuration.GetSection("AuthSettings");
            services.Configure<AuthSettings>(authSettingsSection);
            var authSettings = authSettingsSection.Get<AuthSettings>();

            if (authSettings == null || string.IsNullOrEmpty(authSettings.Key))
            {
                throw new InvalidOperationException("JWT key is not configured. Please check your appsettings.json file.");
            }

            // JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // Set to true in production
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authSettings.Issuer,
                    ValidAudience = authSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Add Authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(UserRoles.Admin));
                options.AddPolicy("RequireManagerRole", policy => policy.RequireRole(UserRoles.Manager));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole(UserRoles.User));
            });

            return services;
        }
    }
}