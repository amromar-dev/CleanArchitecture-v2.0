using CleanArchitectureTemplate.Domain.SampleDomains.Enums;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;

namespace CleanArchitectureTemplate.Domain.SampleDomains.Interfaces
{
    public interface ISampleDomainRepository : IBaseRepository<SampleDomain, int>
    {
        Task<PageList<SampleDomain>> SearchAsync(string search, PageOption<SampleDomainSorting> pagedOptions);
    }
}
