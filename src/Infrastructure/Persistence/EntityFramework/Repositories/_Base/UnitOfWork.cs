using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;
using System.Data;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories.Base
{
    public class UnitOfWork(ApplicationDbContext dbContext, IRequestExecution requestExecution) : IUnitOfWork
    {
        private IDbContextTransaction transaction;

        /// <summary>
        /// Asynchronously commits all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            await DispatchDomainEventsAsync();
        }

        /// <summary>
        /// Create a transaction
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task BeginTransactionAsync()
        {
            if (transaction != null)
                throw new Exception("Transaction already started");

            transaction = await dbContext.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Commit a transaction
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CommitTransactionAsync()
        {
            if (transaction == null)
                throw new Exception("Transaction not started yet");

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            transaction = null;
        }

        /// <summary>
        /// Rollback a transaction
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task RollbackTransactionAsync()
        {
            if (transaction == null)
                throw new Exception("Transaction not started yet");

            await transaction.RollbackAsync();
            await transaction.DisposeAsync();

            transaction = null;
        }

        #region Private Methods

        private async Task DispatchDomainEventsAsync()
        {
            var domainEntities = GetAllDomainEntities();

            foreach (var domainEntity in domainEntities)
            {
                List<IDomainEvent> domainEvents = [.. domainEntity.Entity.DomainEvents];
                domainEntity.Entity.ClearDomainEvents();

                foreach (IDomainEvent domainEvent in domainEvents)
                    await requestExecution.Publishsync(domainEvent);
            }
        }

        private List<EntityEntry<IDomainEntity>> GetAllDomainEntities()
        {
            return dbContext.ChangeTracker.Entries<IDomainEntity>()
                 .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();
        }

        #endregion
    }
}