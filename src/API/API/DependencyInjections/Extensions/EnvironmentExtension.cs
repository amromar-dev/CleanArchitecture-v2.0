using CleanArchitectureTemplate.SharedKernels.Environments;

namespace CleanArchitectureTemplate.API.DependencyInjections
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnvironmentExtension
    {
        /// <summary>
        /// Add default supported localization and culture for the application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureAppEnvironment(this IServiceCollection services, IConfiguration configuration)
        {
            var hostEnvironment = services.BuildServiceProvider().GetService<IWebHostEnvironment>();
            Enum.TryParse<ApplicationEnvironmentType>(hostEnvironment?.EnvironmentName, true, out var environmentType);
            ApplicationEnvironment.CurrentEnvironment = environmentType;
            
            string client = configuration.GetValue<string>("Client");
            ApplicationEnvironment.CurrentClient = Enum.TryParse(client, true, out ApplicationEnvironmentClient clientType) ? clientType : ApplicationEnvironmentClient.Default;
        }
    }
}
