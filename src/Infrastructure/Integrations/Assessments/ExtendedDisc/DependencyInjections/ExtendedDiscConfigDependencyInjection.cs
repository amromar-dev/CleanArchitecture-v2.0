using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Interfaces;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Configurations;
using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.DependencyInjections
{
    public static class ExtendedDiscConfigDependencyInjection
    {
        /// <summary>
        /// Registers the Extended Disc Services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureExtendedDisc(this IServiceCollection services)
        {
            services.AddTransient<IExtendedDiscAssessment, ExtendedDiscService>();
            services.Configure<ExtendedDiscConfig>(out var config);
            services.AddHttpClient(nameof(ExtendedDiscService), x =>
            {
                x.BaseAddress = new Uri(config.BaseAddress);
            });
        }
    }
}
