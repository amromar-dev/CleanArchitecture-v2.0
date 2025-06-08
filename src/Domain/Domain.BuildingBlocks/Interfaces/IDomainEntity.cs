namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    public interface IDomainEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}
