using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public record GetSampleDomainByIdQuery(int Id) : IQuery<SampleDomainOutput>
    {

    }
}
