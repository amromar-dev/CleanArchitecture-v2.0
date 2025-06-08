namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results.Errors
{
    /// <summary>
    /// Represents a error result from a request, including a description and a code.
    /// </summary>
    /// <param name="Message">A description of the error.</param>
    /// <param name="Code">A code representing the type of error.</param>
    public record RequestError(string Message, int Code);
}
