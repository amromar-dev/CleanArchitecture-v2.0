using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Models;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Interfaces
{
	public interface IScheduler
	{
        /// <summary>
        /// Enqueues a job for immediate execution.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the job.</typeparam>
        /// <param name="request">The command job containing the job logic to be executed.</param>
        /// <returns>The unique identifier of the enqueued job.</returns>
        string EnqueueRequest(ICommand request);

        /// <summary>
        /// Schedules a job to run after the specified parent job completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the job.</typeparam>
        /// <param name="parentJobId">The ID of the parent job that must complete before this job is triggered.</param>
        /// <param name="request">The command job containing the job logic to be executed.</param>
        /// <returns>The unique identifier of the scheduled job.</returns>
        string ContinueRequestWith(string parentJobId, ICommand request);

        /// <summary>
        /// Schedules a job to run at a specific future time.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the job.</typeparam>
        /// <param name="request">The command job containing the job logic to be executed.</param>
        /// <param name="enqueueAt">The date and time when the job should be executed.</param>
        /// <returns>The unique identifier of the scheduled job.</returns>
        string ScheduleRequest(ICommand request, DateTimeOffset enqueueAt);

        /// <summary>
        /// Schedules a recurring job to run at regular intervals.
        /// </summary>
        /// <param name="job">The command job containing the job logic to be executed.</param>
        /// <param name="recurringMinutes">The interval in minutes between job executions.</param>
        void Recurring<T>(string jobName, RecurringExpression recurringExpression) where T : IRecurringJob;
    }
}