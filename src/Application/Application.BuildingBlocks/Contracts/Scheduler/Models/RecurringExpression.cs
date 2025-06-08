namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Models
{
    public class RecurringExpression
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        private RecurringExpression(string expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Expression { get; private set; }

        /// <summary>
        /// Returns a cron expression that fires every minute.
        /// </summary>
        /// <returns>A cron expression for a task that runs every minute.</returns>
        public static RecurringExpression Minutely() => new("* * * * *");

        /// <summary>
        /// Returns a cron expression that fires every hour at the first minute.
        /// </summary>
        /// <returns>A cron expression for a task that runs every hour at the first minute.</returns>
        public static RecurringExpression Hourly() => Hourly(0);

        /// <summary>
        /// Returns a cron expression that fires every hour at the specified minute.
        /// </summary>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        /// <returns>A cron expression for a task that runs every hour at the specified minute.</returns>
        public static RecurringExpression Hourly(int minute) => new($"{minute} * * * *");

        /// <summary>
        /// Returns a cron expression that fires every day at 00:00 UTC.
        /// </summary>
        /// <returns>A cron expression for a task that runs daily at 00:00 UTC.</returns>
        public static RecurringExpression Daily() => Daily(0);

        /// <summary>
        /// Returns a cron expression that fires every day at the specified hour in UTC.
        /// </summary>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <returns>A cron expression for a task that runs daily at the specified hour.</returns>
        public static RecurringExpression Daily(int hour) => Daily(hour, 0);

        /// <summary>
        /// Returns a cron expression that fires every day at the specified hour and minute in UTC.
        /// </summary>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        /// <returns>A cron expression for a task that runs daily at the specified hour and minute.</returns>
        public static RecurringExpression Daily(int hour, int minute) => new($"{minute} {hour} * * *");

        /// <summary>
        /// Returns cron expression that fires every <interval> minutes.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static RecurringExpression MinuteInterval(int interval) => new($"*/{interval} * * * *");

        /// <summary>
        /// Returns cron expression that fires every <interval> hours.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static RecurringExpression HourInterval(int interval) => new($"0 */{interval} * * *");
    }
}