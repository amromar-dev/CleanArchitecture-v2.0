using AutoMapper;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Domain.Identity.Users;
using CleanArchitectureTemplate.Domain.Identity.Users.Interfaces;
using CleanArchitectureTemplate.SharedKernels.Exceptions;

namespace CleanArchitectureTemplate.Application.Features.Identity.Account
{
    public class GetUserProfileQueryHandler(IUserRepository userRepository, IMapper mapper, IIdentityContext context) : QueryHandler<GetUserProfileQuery, UserOutput>
    {
        public override async Task<IRequestResult<UserOutput>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            User user = await userRepository.FindAsync(context.GetUserId()) ?? throw new NotFoundException();
            UserOutput result = mapper.Map<UserOutput>(user);

            return Result(result);
        }
    }
}
