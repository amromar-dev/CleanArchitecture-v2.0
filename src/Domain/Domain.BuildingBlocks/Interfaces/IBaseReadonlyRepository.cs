namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    /// <summary>
    /// Defines a contract for a read-only repository that provides access to entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for the entity.</typeparam>
    public interface IBaseReadonlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Retrieves a list of all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
        Task<List<TEntity>> ListAsync();

        /// <summary>
        /// Finds an entity by its primary key asynchronously.
        /// </summary>
        /// <param name="id">The primary key of the entity to find.</param>
        /// <returns>A value task that represents the asynchronous operation. The value task result contains the entity if found, otherwise null.</returns>
        ValueTask<TEntity> FindAsync(TKey id);

        /// <summary>
        /// Counts the total number of entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities.</returns>
        Task<int> CountAsync();
    }
}
