using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions
{
    /// <summary>
    /// Defines an interface for executing commands and queries.
    /// </summary>
    public interface IRequestExecution
    {
        /// <summary>
        /// Executes a command asynchronously and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the command.</typeparam>
        /// <param name="command">The command to be executed.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the outcome of the command execution, encapsulated in a <see cref="RequestResult{TResult}"/>.</returns>
        Task<IRequestResult<TResult>> ExecuteAsync<TResult>(ICommand<TResult> command);

        /// <summary>
        /// Executes a command asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the command.</typeparam>
        /// <param name="command">The command to be executed.</param>
        Task ExecuteAsync(ICommand command);

        /// <summary>
        /// Queries asynchronously and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the query.</typeparam>
        /// <param name="query">The query to be executed.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the outcome of the query, encapsulated in a <see cref="RequestResult{TResult}"/>.</returns>
        Task<IRequestResult<TResult>> QueryAsync<TResult>(IQuery<TResult> query);

        /// <summary>
        /// Publish a notification event asynchronously using the <see cref="IMediator"/> 
        /// </summary>  
        Task Publishsync(IDomainEvent domainEvent);
    }
}