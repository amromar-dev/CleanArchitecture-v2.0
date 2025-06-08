using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML;

namespace CleanArchitectureTemplate.Infrastructure.FileGenerators.HTML.DependencyInjections
{
    public static class HTMLDependencyInjection
    {
        /// <summary>
        /// Registers the pdf services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureHTML(this IServiceCollection services)
        {
            services.AddSingleton<IHtmlRenderService, RazorRenderService>();
        }
    }
}
