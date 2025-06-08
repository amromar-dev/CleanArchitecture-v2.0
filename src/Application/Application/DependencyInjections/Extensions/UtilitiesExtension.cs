using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.SharedKernels.ExportFiles.ExportCSV;

namespace CleanArchitectureTemplate.Application.DependencyInjections.Extensions
{
    public static class UtilitiesExtension
    {
        /// <summary>
        /// Configure request execution lifetime DI
        /// </summary>
        /// <param name="services"></param>
        internal static void ConfigureUtilities(this IServiceCollection services)
        {
            services.AddScoped<ICSVFileUtility, CSVFileUtility>();
        }
    }
}
