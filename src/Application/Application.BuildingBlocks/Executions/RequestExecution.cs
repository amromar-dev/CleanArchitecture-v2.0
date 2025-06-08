using MediatR;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions
{
    public class RequestExecution(IMediator mediator) : IRequestExecution
    {
        /// <summary>
        /// Executes a command asynchronously using the <see cref="IMediator"/> and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the command.</typeparam>
        /// <param name="command">The command to be executed.</param>
        public Task<IRequestResult<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command) => 
            command == null ? throw new ArgumentNullException(nameof(command)) : mediator.Send(command);

        /// <summary>
        /// Executes a command asynchronously using the <see cref="IMediator"/> and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the command.</typeparam>
        /// <param name="command">The command to be executed.</param>
        public Task ExecuteAsync(ICommand command) =>
            command == null ? throw new ArgumentNullException(nameof(command)) : mediator.Send(command);

        /// <summary>
        /// Queries asynchronously using the <see cref="IMediator"/> and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the query.</typeparam>
        /// <param name="query">The query to be executed.</param>
        public Task<IRequestResult<TResult>> QueryAsync<TResult>(IQuery<TResult> query) => 
            query == null ? throw new ArgumentNullException(nameof(query)) : mediator.Send(query);

        /// <summary>
        /// Publish a notification event asynchronously using the <see cref="IMediator"/> 
        /// </summary>      
        public Task Publishsync(IDomainEvent domainEvent) =>
            domainEvent == null ? throw new ArgumentNullException(nameof(domainEvent)) : mediator.Publish(domainEvent);
    }
}