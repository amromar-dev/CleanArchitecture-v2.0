using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.BuildingBlocks.Validation;
using CleanArchitectureTemplate.Application.Pipelines.AccessValidation;

namespace CleanArchitectureTemplate.Application.DependencyInjections.Extensions
{
    public static class MediatRExtension
    {
        /// <summary>
        /// Configure MediatR lifetime DI
        /// </summary>
        /// <param name="services"></param>
        internal static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidatorPipeline<,>));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AccessValidationPipeline<,>));
        }
    }
}
