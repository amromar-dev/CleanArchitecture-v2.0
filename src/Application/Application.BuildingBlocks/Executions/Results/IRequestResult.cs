
namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results
{
    public interface IRequestResult<T>
    {
        /// <summary>
        /// The data resulting from the request, if successful.
        /// </summary>
        T Data { get; init; }

        /// <summary>
        /// A flag indicating whether the request was successful.
        /// </summary>
        bool Success { get; init; }

        /// <summary>
        /// Gets the timestamp of when the result was created.
        /// </summary>
        DateTime Timestamp { get; }
    }
}