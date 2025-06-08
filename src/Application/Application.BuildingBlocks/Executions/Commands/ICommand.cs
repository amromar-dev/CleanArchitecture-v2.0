using MediatR;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands
{
    /// <summary>
    /// Represents a command that can be sent to a handler to perform an action and return a result.
    /// </summary>
    /// <typeparam name="TResult">The type of result that the command will return.</typeparam>
    public interface ICommand<TResult> : IRequest<IRequestResult<TResult>>
    {
    }

    /// <summary>
    /// Represents a command that can be sent to a handler to perform an action.
    /// </summary>
    public interface ICommand : IRequest<IRequestResult<bool>>
    {
    }
}