using CleanArchitectureTemplate.SharedKernels.Exceptions.Base;
using CleanArchitectureTemplate.SharedKernels.Localizations;

namespace CleanArchitectureTemplate.SharedKernels.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message = null) : base(message ?? Localization.NotFoundException)
        {

        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public override int ExceptionCode => 404;
    }
}
