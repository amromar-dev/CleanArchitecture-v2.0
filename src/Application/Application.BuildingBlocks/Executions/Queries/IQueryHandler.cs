using MediatR;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries
{
    /// <summary>
    /// Defines a handler for processing queries and returning results.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query that this handler can process. Must implement <see cref="IQuery{TResult}"/>.</typeparam>
    /// <typeparam name="TResult">The type of result that the query handler returns.</typeparam>
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, IRequestResult<TResult>>
        where TQuery : IQuery<TResult>
    {
    }
}