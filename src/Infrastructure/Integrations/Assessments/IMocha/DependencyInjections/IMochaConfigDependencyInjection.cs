using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Interfaces;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Configurations;
using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.DependencyInjections
{
    public static class IMochaConfigDependencyInjection
    {
        /// <summary>
        /// Registers the IMocha Services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIMocha(this IServiceCollection services)
        {
            services.AddTransient<IIMochaAssessment, IMochaService>();
            services.Configure<IMochaConfig>(out var config);
            services.AddHttpClient(nameof(IMochaService), x =>
            {
                x.BaseAddress = new Uri(config.BaseAddress);
            });
        }
    }
}
