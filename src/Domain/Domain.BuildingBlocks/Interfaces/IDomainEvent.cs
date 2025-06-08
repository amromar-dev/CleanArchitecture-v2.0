using MediatR;

namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    /// <summary>
    /// Represents a domain event in the system. Domain events are used to notify other parts of the system
    /// when something of interest occurs in the domain layer. This interface extends <see cref="INotification"/>
    /// to integrate with the MediatR library for event handling.
    /// </summary>
    public interface IDomainEvent : INotification
    {
        
    }
}
