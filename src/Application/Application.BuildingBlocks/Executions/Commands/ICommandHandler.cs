using MediatR;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands
{
    /// <summary>
    /// Defines a handler for processing commands and returning results.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command that this handler can process. Must implement <see cref="ICommand{TResult}"/>.</typeparam>
    /// <typeparam name="TResult">The type of result that the command handler returns.</typeparam>
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, IRequestResult<TResult>>
        where TCommand : ICommand<TResult>
    {
    }

    /// <summary>
    /// Defines a handler for processing commands and returning results.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command that this handler can process. Must implement <see cref="ICommand{TResult}"/>.</typeparam>
    /// <typeparam name="TResult">The type of result that the command handler returns.</typeparam>
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, IRequestResult<bool>>
        where TCommand : ICommand
    {
    }
}