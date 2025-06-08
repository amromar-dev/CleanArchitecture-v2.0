using AutoMapper;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.Domain.SampleDomains;
using CleanArchitectureTemplate.Domain.SampleDomains.Interfaces;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public class GetSampleDomainPagedQueryHandler(ISampleDomainRepository  sampleDomainRepository, IMapper mapper) : QueryHandler<GetSampleDomainPagedQuery, PageList<SampleDomainOutput>>
    {
        public override async Task<IRequestResult<PageList<SampleDomainOutput>>> Handle(GetSampleDomainPagedQuery request, CancellationToken cancellationToken)
        {
            PageList<SampleDomain> sampleDomains = await sampleDomainRepository.SearchAsync(request.Search, request.PagedOptions);
            PageList<SampleDomainOutput> results = mapper.Map<PageList<SampleDomainOutput>>(sampleDomains);

            return Result(results);
        }
    }
}
