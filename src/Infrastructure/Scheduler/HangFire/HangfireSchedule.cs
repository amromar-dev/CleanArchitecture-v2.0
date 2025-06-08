using Hangfire;
using Newtonsoft.Json;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Interfaces;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Models;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions;

namespace CleanArchitectureTemplate.Infrastructure.Scheduler.Hangfire
{
    public class HangfireSchedule(IRequestExecution requestExecution) : IScheduler
    {
        /// <summary>
        /// Enqueues a job for immediate execution.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the job.</typeparam>
        /// <param name="request">The command request containing the job logic to be executed.</param>
        /// <returns>The unique identifier of the enqueued job.</returns>
        public string EnqueueRequest(ICommand request)
		{
			return BackgroundJob.Enqueue(() => new HangfireSchedule(requestExecution).ExecuteRequest(request.GetType(), JsonConvert.SerializeObject(request)));
		}

        /// <summary>
        /// Schedules a job to run after the specified parent job completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the job.</typeparam>
        /// <param name="parentJobId">The ID of the parent job that must complete before this job is triggered.</param>
        /// <param name="request">The command request containing the job logic to be executed.</param>
        /// <returns>The unique identifier of the scheduled job.</returns>
        public string ContinueRequestWith(string parentJobId, ICommand request)
		{
			return BackgroundJob.ContinueJobWith(parentJobId, () => new HangfireSchedule(requestExecution).ExecuteRequest(request.GetType(), JsonConvert.SerializeObject(request)));
		}

        /// <summary>
        /// Schedules a job to run at a specific future time.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the job.</typeparam>
        /// <param name="request">The command request containing the job logic to be executed.</param>
        /// <param name="enqueueAt">The date and time when the job should be executed.</param>
        /// <returns>The unique identifier of the scheduled job.</returns>
        public string ScheduleRequest(ICommand request, DateTimeOffset scheduleAt)
		{
			return BackgroundJob.Schedule(() => new HangfireSchedule(requestExecution).ExecuteRequest(request.GetType(), JsonConvert.SerializeObject(request)), scheduleAt);
		}

        /// <summary>
        /// Schedules a recurring job to run at regular intervals.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the job.</typeparam>
        /// <param name="jobName">The unique name of the recurring job.</param>
        /// <param name="request">The command request containing the job logic to be executed.</param>
        /// <param name="recurringMinutes">The interval in minutes between job executions.</param>
        public void Recurring<T>(string jobName, RecurringExpression recurringExpression) where T : IRecurringJob
        {
            RecurringJob.AddOrUpdate<T>(jobName, ex => ex.Execute(), recurringExpression.Expression);
        }

        /// <summary>
        /// Execute the request
        /// </summary>
        /// <param name="type"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public Task ExecuteRequest(Type type, string json)
        {
            return JsonConvert.DeserializeObject(json, type) is ICommand command
                ? requestExecution.ExecuteAsync(command)
                : Task.CompletedTask;
        }
    }
}
