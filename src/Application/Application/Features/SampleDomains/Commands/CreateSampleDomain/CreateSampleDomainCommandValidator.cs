using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public class CreateSampleDomainCommandValidator : AbstractValidator<CreateSampleDomainCommand>
    {
        public CreateSampleDomainCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Status).IsInEnum(); 
        }
    }
}
