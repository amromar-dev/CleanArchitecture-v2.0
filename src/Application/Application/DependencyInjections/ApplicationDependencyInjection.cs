using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.DependencyInjections.Extensions;

namespace CleanArchitectureTemplate.Application.DependencyInjections
{
    public static class ApplicationDependencyInjection
    {
        /// <summary>
        /// Configure application services lifetime DI
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.ConfigureMediatR();
            services.ConfigureAutoMapper();
            services.ConfigureExecutions();
            services.ConfigureUtilities();
        }
    }
}