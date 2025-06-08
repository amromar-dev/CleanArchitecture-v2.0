using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.DependencyInjections.Extensions;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.DependencyInjections
{
    public static class EntityFrameworkDependencyInjection
    {
        /// <summary>
        /// Configures Entity Framework Core and registers repository services with the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        /// <param name="connectionString">The connection string for the database. This is used to configure the <see cref="DbContext"/>.</param>
        public static void ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRepositories();
            services.ConfigureDbContext(configuration);
        }

        /// <summary>
        /// Extension method for initializing Entity Framework within the application.
        /// This method is intended to be called during the application startup to ensure
        /// </summary>
        /// <param name="app">The application builder instance used to configure the HTTP request pipeline.</param>
        public static void InitializeEntityFramework(this IApplicationBuilder app)
        {
            app.MigrateDatabase();
            app.SeedsDatabase();
        }
    }
}
