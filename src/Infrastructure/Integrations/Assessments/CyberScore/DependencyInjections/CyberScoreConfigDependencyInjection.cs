using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Interfaces;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Configurations;
using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.DependencyInjections
{
    public static class CyberScoreConfigDependencyInjection
    {
        /// <summary>
        /// Registers the CyberScore Services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCyberScore(this IServiceCollection services)
        {
            services.AddTransient<ICyberScoreAssessment, CyberScoreService>();
            services.Configure<CyberScoreConfig>(out var config);
            services.AddHttpClient(nameof(CyberScoreService), x =>
            {
                x.BaseAddress = new Uri(config.BaseAddress);
            });
        }
    }
}
