using CleanArchitectureTemplate.SharedKernels.Exceptions;

namespace CleanArchitectureTemplate.API.DependencyInjections
{
    /// <summary>
    /// 
    /// </summary>
    public static class APIDependencyInjection
    {
        /// <summary>
        /// Extension method for configuring API-related services in the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(ms => ms.Value.Errors.Count > 0)
                        .SelectMany(ms => ms.Value.Errors.Where(e => e.Exception == null).Select(error => $"'{ms.Key}' {error.ErrorMessage}"))
                        .ToList();

                    var exceptions = context.ModelState.Where(ms => ms.Value.Errors.Count > 0)
                       .SelectMany(ms => ms.Value.Errors.Where(e => e.Exception != null).Select(error => $"'{ms.Key}' {error.Exception.Message}"))
                       .ToList();

                    errors.AddRange(exceptions);
                    throw new FieldsValidationException(errors);
                };
            }); 

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.ConfigureAppEnvironment(configuration);
            services.ConfigureSwagger(configuration);
            services.ConfigureCorsPolicy();
            services.ConfigureLocalizations();
        }
    }
}
