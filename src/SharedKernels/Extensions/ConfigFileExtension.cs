using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using CleanArchitectureTemplate.SharedKernels.Environments;

namespace CleanArchitectureTemplate.Common.SharedKernels.Utilities.Externsions
{
    public static class ConfigDependencyExtension
    {
        public static TModel GetOptionsFromFile<TModel>(this IServiceCollection services, string fileName) where TModel : class
        {
            var path = Path.Combine(AppContext.BaseDirectory, $"{fileName}.{ApplicationEnvironment.CurrentEnvironment}.json");
            if (File.Exists(path) == false)
                path = Path.Combine(AppContext.BaseDirectory, $"{fileName}.json");

            var json = File.ReadAllText(path);
           
            var options = JsonConvert.DeserializeObject<TModel>(json);
            services.AddSingleton(options);
            return options;
        }
    }
}
