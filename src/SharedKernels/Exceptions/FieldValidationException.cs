using CleanArchitectureTemplate.SharedKernels.Exceptions.Base;
using CleanArchitectureTemplate.SharedKernels.Localizations;

namespace CleanArchitectureTemplate.SharedKernels.Exceptions
{
    public class FieldValidationException(string FieldName, string Exception = null) : BaseException($"'{FieldName}' {Exception ?? Localization.Required}")
    {
        public override int ExceptionCode => 400;
    }
}
