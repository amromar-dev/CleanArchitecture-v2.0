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
    public class Repository<TEntity>(ApplicationDbContext dbContext) : BaseRepository<TEntity, int>(dbContext), IRepository<TEntity> where TEntity : Entity<int>
    {
        
    }
}

