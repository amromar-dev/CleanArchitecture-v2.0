using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Interfaces;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.PersistedGrants
{
    public class TokenCleanupBackgroundService(IServiceProvider services, ILogger<TokenCleanupBackgroundService> logger) : IRecurringJob
    {
        public async Task Execute()
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            const int TokenCleanupBatchSize = 5_00;
            int batchMaxSize = int.MaxValue;

            while (batchMaxSize >= TokenCleanupBatchSize)
            {
                var expiredGrants = await dbContext.Set<PersistedGrant>()
                    .Where(x => x.Expiration < DateTime.UtcNow)
                    .OrderBy(x => x.Expiration)
                    .Take(TokenCleanupBatchSize)
                    .ToArrayAsync();

                batchMaxSize = expiredGrants.Length;
                logger.LogInformation("Removing {grantCount} grants", batchMaxSize);

                if (batchMaxSize > 0)
                {
                    dbContext.Set<PersistedGrant>().RemoveRange(expiredGrants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
