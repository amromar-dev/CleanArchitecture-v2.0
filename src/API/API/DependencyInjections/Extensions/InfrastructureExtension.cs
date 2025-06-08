using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.DependencyInjections;
using CleanArchitectureTemplate.Infrastructure.FileStorage.FileLocalStorage.DependencyInjections;
using CleanArchitectureTemplate.Infrastructure.Scheduler.Hangfire.DependencyInjections;
using CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.DependencyInjections;
using CleanArchitectureTemplate.Infrastructure.FileGenerators.PDF.DependencyInjections;
using CleanArchitectureTemplate.Infrastructure.FileGenerators.HTML.DependencyInjections;
using CleanArchitectureTemplate.Infrastructure.Email.Smtp.DependencyInjections;

namespace CleanArchitectureTemplate.API.DependencyInjections
{
    /// <summary>
    /// 
    /// </summary>
    public static class InfrastructureExtension
    {
        /// <summary>
        /// Extension method for configuring the infrastructure services in the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureEntityFramework(configuration);
            services.ConfigureIdentityServer(configuration);
            services.ConfigureHangfire(configuration);
            services.ConfigureFileLocalStorage();
            services.ConfigurePDF();
            services.ConfigureHTML();
            services.ConfigureSmtp();
        }

        /// <summary>
        /// Extension method for initializing the infrastructure components of the application.
        /// </summary>
        /// <param name="app"></param>
        public static void InitializeInfrastructure(this IApplicationBuilder app)
        {
            app.InitializeEntityFramework();
            app.UseHangFire();
        }
    }
}
