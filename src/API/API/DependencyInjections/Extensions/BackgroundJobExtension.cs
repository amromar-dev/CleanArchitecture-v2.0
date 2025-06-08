using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Interfaces;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Models;
using CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.PersistedGrants;

namespace CleanArchitectureTemplate.API.DependencyInjections
{
    /// <summary>
    /// 
    /// </summary>
    public static class BackgroundJobExtension
    {
        /// <summary>
        /// Adds or updates a recurring job using the specified recurring expression.
        /// </summary>
        public static void InitializeBackgroundJobs(this IApplicationBuilder app)
        {
            //app.AddOrUpdate<DeactiveExpiredSampleBackgroundJob>(RecurringExpression.MinuteInterval(3));
            app.AddOrUpdate<TokenCleanupBackgroundService>(RecurringExpression.Daily(1));
        }

        #region Private Methods
       
        private static void AddOrUpdate<T>(this IApplicationBuilder app, RecurringExpression recurringExpression) where T : class, IRecurringJob
        {
            var scope = app.ApplicationServices.CreateScope();
            var scheduler = scope.ServiceProvider.GetRequiredService<IScheduler>();

            scheduler.Recurring<T>(typeof(T).Name, recurringExpression);
        } 

        #endregion
    }
}
