using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Domain.SampleDomains.Events
{
    public record SampleDomainActivatedEvent(SampleDomain Assessment) : IDomainEvent
    {
        
    }
}
