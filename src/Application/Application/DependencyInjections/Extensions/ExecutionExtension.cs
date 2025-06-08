using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions;

namespace CleanArchitectureTemplate.Application.DependencyInjections.Extensions
{
    public static class ExecutionExtension
    {
        /// <summary>
        /// Configure request execution lifetime DI
        /// </summary>
        /// <param name="services"></param>
        internal static void ConfigureExecutions(this IServiceCollection services)
        {
            services.AddScoped<IRequestExecution, RequestExecution>();
            services.AddValidatorsFromAssembly(typeof(ExecutionExtension).Assembly);
        }
    }
}
