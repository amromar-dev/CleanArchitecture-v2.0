using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework;
using EntitiesPersistedGrant = CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models.PersistedGrant;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.PersistedGrants
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<PersistedGrantStore> logger;
        private readonly DbSet<EntitiesPersistedGrant> persistedGrantSet;

        public PersistedGrantStore(ApplicationDbContext context, ILogger<PersistedGrantStore> logger)
        {
            this.context = context;
            this.persistedGrantSet = context.Set<EntitiesPersistedGrant>();
            this.logger = logger;
        }

        public virtual async Task StoreAsync(PersistedGrant token)
        {
            var existing = (await persistedGrantSet.Where(x => x.Key == token.Key).ToArrayAsync()).SingleOrDefault(x => x.Key == token.Key);
            if (existing == null)
            {
                var persistedGrant = token.ToEntity();
                persistedGrantSet.Add(persistedGrant);
            }
            else
            {
                token.UpdateEntity(existing);
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogWarning("exception updating {persistedGrantKey} persisted grant in database: {error}", token.Key, ex.Message);
            }
        }

        public virtual async Task<PersistedGrant> GetAsync(string key)
        {
            var persistedGrant = (await persistedGrantSet.AsNoTracking().Where(x => x.Key == key).ToArrayAsync())   
                .SingleOrDefault(x => x.Key == key);
            var model = persistedGrant?.ToModel();

            logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, model != null);

            return model;
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            filter.Validate();

            var persistedGrants = await Filter(persistedGrantSet.AsQueryable(), filter).ToArrayAsync();
            persistedGrants = [.. Filter(persistedGrants.AsQueryable(), filter)];

            var model = persistedGrants.Select(x => x.ToModel());

            logger.LogDebug("{persistedGrantCount} persisted grants found for {@filter}", persistedGrants.Length, filter);

            return model;
        }

        public virtual async Task RemoveAsync(string key)
        {
            var persistedGrant = (await persistedGrantSet.Where(x => x.Key == key).ToArrayAsync()).SingleOrDefault(x => x.Key == key);
            if (persistedGrant != null)
            {
                persistedGrantSet.Remove(persistedGrant);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    logger.LogInformation("exception removing {persistedGrantKey} persisted grant from database: {error}", key, ex.Message);
                }
            }
            else
            {
                logger.LogDebug("no {persistedGrantKey} persisted grant found in database", key);
            }
        }

        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            filter.Validate();

            var persistedGrants = await Filter(persistedGrantSet.AsQueryable(), filter).ToArrayAsync();
            persistedGrants = Filter(persistedGrants.AsQueryable(), filter).ToArray();

            logger.LogDebug("removing {persistedGrantCount} persisted grants from database for {@filter}", persistedGrants.Length, filter);

            persistedGrantSet.RemoveRange(persistedGrants);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogInformation("removing {persistedGrantCount} persisted grants from database for subject {@filter}: {error}", persistedGrants.Length, filter, ex.Message);
            }
        }

        private IQueryable<EntitiesPersistedGrant> Filter(IQueryable<EntitiesPersistedGrant> query, PersistedGrantFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.ClientId))
            {
                query = query.Where(x => x.ClientId == filter.ClientId);
            }
            if (!string.IsNullOrWhiteSpace(filter.SessionId))
            {
                query = query.Where(x => x.SessionId == filter.SessionId);
            }
            if (!string.IsNullOrWhiteSpace(filter.SubjectId))
            {
                query = query.Where(x => x.SubjectId == filter.SubjectId);
            }
            if (!string.IsNullOrWhiteSpace(filter.Type))
            {
                query = query.Where(x => x.Type == filter.Type);
            }

            return query;
        }
    }
}
