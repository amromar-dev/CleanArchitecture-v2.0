using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories.Base
{
    /// <summary>
    /// Generic base repository class for handling basic read-only operations on entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <param name="dbContext"></param>
    public class BaseRepository<TEntity, Key>(ApplicationDbContext dbContext) : BaseReadonlyRepository<TEntity, Key>(dbContext),
        IBaseRepository<TEntity, Key> where TEntity : Entity<Key>
    {
        /// <summary>
        /// Adds a single entity to the DbSet for insertion into the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Adds a list of entities to the DbSet for insertion into the database.
        /// </summary>
        /// <param name="entities">The list of entities to add.</param>
        public void Add(IList<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        /// <summary>
        /// Removes a single entity from the DbSet for deletion from the database.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Removes a list of entities from the DbSet for deletion from the database.
        /// </summary>
        /// <param name="entities">The list of entities to remove.</param>
        public void Remove(IList<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Updates a single entity in the DbSet.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        /// <summary>
        /// Updates a list of entities in the DbSet.
        /// </summary>
        /// <param name="entities">The list of entities to update.</param>
        public void Update(IList<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }
    }
}

