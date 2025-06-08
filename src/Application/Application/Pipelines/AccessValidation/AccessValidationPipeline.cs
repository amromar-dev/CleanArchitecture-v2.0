using MediatR;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity;

namespace CleanArchitectureTemplate.Application.Pipelines.AccessValidation
{
    public class AccessValidationPipeline<TRequest, TResponse>(IIdentityContext context) : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IAccessValidationValidation _)
            {
                /// Implement Validation 
            }

            return await next();
        }
    }
}
