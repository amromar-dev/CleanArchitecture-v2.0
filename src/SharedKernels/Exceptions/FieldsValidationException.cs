using CleanArchitectureTemplate.SharedKernels.Exceptions.Base;
using CleanArchitectureTemplate.SharedKernels.Localizations;

namespace CleanArchitectureTemplate.SharedKernels.Exceptions
{
    public class FieldsValidationException(List<string> validations) : BaseException(Localization.FieldsValidationError)
    {
        public override int ExceptionCode => 400;

        public List<string> Validations { get; } = validations;
    }
}
