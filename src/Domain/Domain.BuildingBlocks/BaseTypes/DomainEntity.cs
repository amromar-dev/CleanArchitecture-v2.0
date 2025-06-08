using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DomainEntity<Key>(int actionUserId = default) : AuditableEntity<Key>(actionUserId), IDomainEntity
    {
        private List<IDomainEvent> domainEvents;

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents?.AsReadOnly();

        /// <summary>
        /// Clear dmain events
        /// </summary>
        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent">Domain event.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents ??= [];
            domainEvents.Add(domainEvent);
        }
    }
}
