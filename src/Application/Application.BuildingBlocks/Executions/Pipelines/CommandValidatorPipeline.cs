using FluentValidation;
using MediatR;
using CleanArchitectureTemplate.SharedKernels.Exceptions;

namespace Application.BuildingBlocks.Validation
{
    public sealed class CommandValidatorPipeline<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any() == false) 
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationFailures = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures.Where(res => !res.IsValid).SelectMany(res => res.Errors)
                .Select(res => res.ErrorMessage).ToList();

            return errors.Count == 0 ? await next() : throw new FieldsValidationException(errors);
        }
    }
}
