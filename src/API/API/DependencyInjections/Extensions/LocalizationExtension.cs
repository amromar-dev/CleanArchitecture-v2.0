namespace CleanArchitectureTemplate.API.DependencyInjections
{
    /// <summary>
    /// 
    /// </summary>
    public static class LocalizationExtension
    {
        /// <summary>
        /// Add default supported localization and culture for the application
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLocalizations(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "ar" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });
        }
    }
}
