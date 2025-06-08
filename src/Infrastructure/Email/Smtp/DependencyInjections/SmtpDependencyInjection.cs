using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.SharedKernels.DependencyInjections;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Email.Interfaces;
using CleanArchitectureTemplate.Infrastructure.Email.Smtp.Services;
using CleanArchitectureTemplate.Infrastructure.Email.Smtp.Configurations;

namespace CleanArchitectureTemplate.Infrastructure.Email.Smtp.DependencyInjections
{
    public static class SmtpDependencyInjection
    {
        /// <summary>
        /// Registers the file storage services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSmtp(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, SmtpService>();
            services.Configure<SmtpConfig>();
        }
    }
}
