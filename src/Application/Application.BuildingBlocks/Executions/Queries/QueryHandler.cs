using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries
{
    /// <summary>
    /// Defines a handler for processing queries and returning results.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query that this handler can process. Must implement <see cref="IQuery{TResult}"/>.</typeparam>
    /// <typeparam name="TResult">The type of result that the query handler returns.</typeparam>
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public abstract Task<IRequestResult<TResult>> Handle(TQuery request, CancellationToken cancellationToken);

        public IRequestResult<TResult> Result(TResult result)
        {
            return RequestResult<TResult>.SuccessResponse(result);
        }

        public IRequestResult<RequestFile> ResultFile(byte[] data, RequestFileTypes type, string fileName, bool includeTime = true)
        {
            return RequestResult<RequestFile>.SuccessResponse(new RequestFile(data, type, fileName, includeTime));
        }
    }
}