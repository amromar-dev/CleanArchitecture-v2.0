using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.Pipelines.AccessValidation;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.Domain.SampleDomains.Enums;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public record GetSampleDomainPagedQuery : IQuery<PageList<SampleDomainOutput>>, IAccessValidationValidation
    {
        public GetSampleDomainPagedQuery(string search, PageOption<SampleDomainSorting> pagedOptions)
        {
            PagedOptions = pagedOptions;
            Search = search;
        }

        public PageOption<SampleDomainSorting> PagedOptions { get; set; }
        public string Search { get; set; }
    }
}
