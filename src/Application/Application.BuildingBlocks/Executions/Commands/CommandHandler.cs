using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands
{
    /// <summary>
    /// Defines a handler for processing queries and returning results.
    /// </summary>
    /// <typeparam name="TCommand">The type of the Command that this handler can process. Must implement <see cref="ICommand{TResult}"/>.</typeparam>
    /// <typeparam name="TResult">The type of result that the Command handler returns.</typeparam>
    public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        public abstract Task<IRequestResult<TResult>> Handle(TCommand request, CancellationToken cancellationToken);

        public IRequestResult<TResult> Result(TResult result)
        {
            return RequestResult<TResult>.SuccessResponse(result);
        }
    }

    /// <summary>
    /// Defines a handler for processing queries and returning results.
    /// </summary>
    /// <typeparam name="TCommand">The type of the Command that this handler can process. Must implement <see cref="ICommand{TResult}"/>.</typeparam>
    /// <typeparam name="TResult">The type of result that the Command handler returns.</typeparam>
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public abstract Task<IRequestResult<bool>> Handle(TCommand request, CancellationToken cancellationToken);

        public IRequestResult<bool> Result(bool result)
        {
            return RequestResult<bool>.SuccessResponse(result);
        }
    }
}