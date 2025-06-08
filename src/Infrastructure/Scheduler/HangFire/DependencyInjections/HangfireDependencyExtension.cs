using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Interfaces;

namespace CleanArchitectureTemplate.Infrastructure.Scheduler.Hangfire.DependencyInjections
{
    public static class HangfireDependencyExtension
    {
        /// <summary>
        /// Configures Hangfire services for background job processing and server setup.
        /// This method sets up the Hangfire storage using SQL Server, registers a background schedule service,
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IScheduler, HangfireSchedule>();

            var dbConnectionString = configuration.GetConnectionString("DbConnectionString");

            services.AddHangfire(configuration => configuration
                .UseSimpleAssemblyNameTypeSerializer()
                .UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                .UseSqlServerStorage(dbConnectionString));

            services.AddHangfireServer();
        }

        /// <summary>
        /// Configures the Hangfire dashboard middleware to display the dashboard at a specified route.
        /// The method also sets up basic authentication for accessing the dashboard.
        /// </summary>
        /// <param name="app"></param>
        public static void UseHangFire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter { User = "admin", Pass = "admin@123" }
                }
            });
        }
    }
}
