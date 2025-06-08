using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using CleanArchitectureTemplate.SharedKernels.Environments;
using CleanArchitectureTemplate.SharedKernels.Exceptions;

namespace CleanArchitectureTemplate.SharedKernels.DependencyInjections
{
    public static class ConfigureConfigExtension
    {
        /// <summary>
        /// Loads config from a JSON file and registers them as a singleton service in the dependency injection container.
        /// The method first tries to load the config from an environment-specific file (e.g., "fileName.Development.json").
        /// If that file does not exist, it falls back to a default file (e.g., "fileName.json").
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="services"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void Configure<TModel>(this IServiceCollection services) where TModel : class, IConfig, new()
        {
            TModel config = ParseConfig<TModel>();
            services.AddSingleton(config);
        }

        /// <summary>
        /// Loads config from a JSON file and registers them as a singleton service in the dependency injection container.
        /// The method first tries to load the config from an environment-specific file (e.g., "fileName.Development.json").
        /// If that file does not exist, it falls back to a default file (e.g., "fileName.json").
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="services"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void Configure<TConfig>(this IServiceCollection services, out TConfig config) where TConfig : class, IConfig, new()
        {
            config = ParseConfig<TConfig>();
            services.TryAddSingleton(config);
        }

        #region Private Methods

        private static TConfig ParseConfig<TConfig>() where TConfig : class, IConfig, new()
        {
            var config = new TConfig();

            var path = Path.Combine(AppContext.BaseDirectory, $"{config.JsonFileName}.{ApplicationEnvironment.CurrentEnvironment}.json");
            if (File.Exists(path) == false)
                path = Path.Combine(AppContext.BaseDirectory, $"{config.JsonFileName}.json");

            if (File.Exists(path) == false)
                throw new NotFoundException($"{typeof(TConfig)} - {config.JsonFileName}");

            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<TConfig>(json);
        } 

        #endregion
    }

    public interface IConfig
    {
        /// <summary>
        /// Define appconfig file name
        /// </summary>
        string JsonFileName { get; }
    }
}
