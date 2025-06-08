using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands;
using CleanArchitectureTemplate.Application.Pipelines.AccessValidation;
using CleanArchitectureTemplate.Domain.SampleDomains.Enums;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public record CreateSampleDomainCommand : ICommand<int>, IAccessValidationValidation
    {
        public string Name { get; set; }

        public SampleDomainStatus Status { get; set; }

        public string Description { get; set; }
    }
}
