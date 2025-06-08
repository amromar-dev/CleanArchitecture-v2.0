namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Scheduler.Interfaces
{
    public interface IRecurringJob 
    {
        Task Execute();
    }
}
