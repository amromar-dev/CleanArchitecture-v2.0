namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    /// <summary>
    /// Defines a contract for a read-only repository that provides access to entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for the entity.</typeparam>
    public interface IRepository<TEntity> : IBaseRepository<TEntity, int> where TEntity : IEntity<int>
    {

    }
}
