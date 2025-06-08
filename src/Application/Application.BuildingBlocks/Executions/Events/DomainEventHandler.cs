using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Events
{
    /// <summary>
    /// Defines a handler for processing events
    /// </summary>
    public abstract class DomainEventHandler<TEvent> : IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        public abstract Task Handle(TEvent notification, CancellationToken cancellationToken);
    }
}