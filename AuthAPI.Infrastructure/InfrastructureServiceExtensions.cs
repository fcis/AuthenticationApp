using AuthAPI.Domain.Interfaces;
using AuthAPI.Domain.Interfaces.Repositories;
using AuthAPI.Infrastructure.Identity;
using AuthAPI.Infrastructure.Identity.Services;
using AuthAPI.Infrastructure.Persistence;
using AuthAPI.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace AuthAPI.Infrastructure
{
    /// <summary>
    /// Extension methods for registering infrastructure services
    /// </summary>
    public static class InfrastructureServiceExtensions
    {
        /// <summary>
        /// Adds infrastructure services to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The application configuration</param>
        /// <returns>The service collection for method chaining</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Add Identity services
            services.AddIdentityServices(configuration);

            // Register repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register HttpContextAccessor for CurrentUserService
            services.AddHttpContextAccessor();

            // Register CurrentUserService
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}