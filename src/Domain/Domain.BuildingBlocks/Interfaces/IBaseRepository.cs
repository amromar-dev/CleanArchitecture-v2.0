namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    public interface IBaseRepository<TEntity, TKey> : IBaseReadonlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
	{
        /// <summary>
        /// Adds a single entity to the DbSet for insertion into the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds a list of entities to the DbSet for insertion into the database.
        /// </summary>
        /// <param name="entities">The list of entities to add.</param>
        void Add(IList<TEntity> entities);

        /// <summary>
        /// Removes a single entity from the DbSet for deletion from the database.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes a list of entities from the DbSet for deletion from the database.
        /// </summary>
        /// <param name="entities">The list of entities to remove.</param>
        void Remove(IList<TEntity> entities);

        /// <summary>
        /// Updates a single entity in the DbSet.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates a list of entities in the DbSet.
        /// </summary>
        /// <param name="entities">The list of entities to update.</param>
        void Update(IList<TEntity> entities);
    }
}
