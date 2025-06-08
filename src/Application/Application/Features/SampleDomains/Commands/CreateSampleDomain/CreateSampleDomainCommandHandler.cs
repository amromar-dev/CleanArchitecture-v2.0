using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;
using CleanArchitectureTemplate.Domain.SampleDomains;
using CleanArchitectureTemplate.Domain.SampleDomains.Interfaces;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public class CreateSampleDomainCommandHandler(ISampleDomainRepository sampleDomainRepository, IUnitOfWork unitOfWork, IIdentityContext context) : CommandHandler<CreateSampleDomainCommand, int>
    {
        public override async Task<IRequestResult<int>> Handle(CreateSampleDomainCommand request, CancellationToken cancellationToken)
        {
            SampleDomain sampleDomain = new SampleDomain(request.Name, request.Status, request.Description, context.GetUserId());

            sampleDomainRepository.Add(sampleDomain);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result(sampleDomain.Id);
        }
    }
}
