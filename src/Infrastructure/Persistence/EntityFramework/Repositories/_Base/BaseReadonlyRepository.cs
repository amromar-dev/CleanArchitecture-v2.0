using Microsoft.EntityFrameworkCore;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories.Base
{
    /// <summary>
    /// Generic base repository class for handling basic read-only operations on entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="dbContext"></param>
    public class BaseReadonlyRepository<TEntity, TKey>(ApplicationDbContext dbContext) : IBaseReadonlyRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        protected readonly ApplicationDbContext dbContext = dbContext;
        protected readonly DbSet<TEntity> dbSet = dbContext.Set<TEntity>();

        /// <summary>
        /// Asynchronously retrieves all entities from the database.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation. The task result contains a list of entities.</returns>
        public virtual Task<List<TEntity>> ListAsync()
        {
            return dbSet.ToListAsync();
        }

        /// <summary>
        /// Asynchronously finds an entity by its key.
        /// </summary>
        /// <param name="id">The key (ID) of the entity to find.</param>
        /// <returns>A ValueTask representing the asynchronous operation. The task result contains the entity if found.</returns>
        public virtual ValueTask<TEntity> FindAsync(TKey id)
        {
            return dbSet.FindAsync(id);
        }

        /// <summary>
        /// Asynchronously counts the total number of entities in the DbSet.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation. The task result contains the count of entities.</returns>
        public virtual Task<int> CountAsync()
        {
            return dbSet.CountAsync();
        }

        #region Protected Methods

        /// <summary>
        /// Asynchronously retrieves a paginated result set from the provided query.
        /// </summary>
        /// <param name="query">The IQueryable used to retrieve the entities.</param>
        /// <param name="pageNumber">The page number to retrieve (defaults to 1).</param>
        /// <param name="pageSize">The number of items per page (defaults to 10).</param>
        /// <returns>A Task representing the asynchronous operation. The task result contains the paginated result.</returns>
        protected async Task<PageList<TEntity>> GetPagedResultAsync(IQueryable<TEntity> query, int pageNumber = 1, int pageSize = 10)
        {
            var skip = (pageNumber - 1) * pageSize;

            var total = await query.CountAsync();
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return new PageList<TEntity>(items, total, pageNumber, pageSize);
        }

        /// <summary>
        /// Asynchronously retrieves a paginated result set from the provided query.
        /// </summary>
        /// <param name="query">The IQueryable used to retrieve the entities.</param>
        /// <param name="pageNumber">The page number to retrieve (defaults to 1).</param>
        /// <param name="pageSize">The number of items per page (defaults to 10).</param>
        /// <returns>A Task representing the asynchronous operation. The task result contains the paginated result.</returns>
        protected async Task<PageList<T>> GetPagedResultAsync<T>(IQueryable<T> query, int pageNumber = 1, int pageSize = 10)
        {
            var skip = (pageNumber - 1) * pageSize;

            var total = await query.CountAsync();
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return new PageList<T>(items, total, pageNumber, pageSize);
        }

        /// <summary>
        /// Asynchronously retrieves a paginated result set from the provided query.
        /// </summary>
        /// <param name="query">The IQueryable used to retrieve the entities.</param>
        /// <param name="pageNumber">The page number to retrieve (defaults to 1).</param>
        /// <param name="pageSize">The number of items per page (defaults to 10).</param>
        /// <returns>A Task representing the asynchronous operation. The task result contains the paginated result.</returns>
        protected async Task<List<T>> GetPagedAsync<T>(IQueryable<T> query, int pageNumber = 1, int pageSize = 10)
        {
            var skip = (pageNumber - 1) * pageSize;
            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }

        #endregion
    }
}

