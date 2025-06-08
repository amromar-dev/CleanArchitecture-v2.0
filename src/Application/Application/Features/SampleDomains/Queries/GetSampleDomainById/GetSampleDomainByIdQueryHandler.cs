using AutoMapper;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Domain.SampleDomains;
using CleanArchitectureTemplate.Domain.SampleDomains.Interfaces;
using CleanArchitectureTemplate.SharedKernels.Exceptions;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public class GetSampleDomainByIdQueryHandler(ISampleDomainRepository sampleDomainRepository, IMapper mapper) : QueryHandler<GetSampleDomainByIdQuery, SampleDomainOutput>
    {
        public override async Task<IRequestResult<SampleDomainOutput>> Handle(GetSampleDomainByIdQuery request, CancellationToken cancellationToken)
        {
            SampleDomain sampleDomain = await sampleDomainRepository.FindAsync(request.Id) ?? throw new NotFoundException();
            SampleDomainOutput result = mapper.Map<SampleDomainOutput>(sampleDomain);
            return Result(result);
        }
    }
}
