namespace CleanArchitectureTemplate.API.DependencyInjections
{
    /// <summary>
    /// 
    /// </summary>
    public static class CorsPolicyExtension
    {
        /// <summary>
        /// Adds cross-origin resource sharing services to the specified
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition");
                });
            });
        }
    }
}
