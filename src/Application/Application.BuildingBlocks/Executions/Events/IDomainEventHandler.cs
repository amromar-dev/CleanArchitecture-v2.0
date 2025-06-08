using MediatR;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Events
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
