using CleanArchitectureTemplate.SharedKernels.Exceptions.Base;

namespace CleanArchitectureTemplate.SharedKernels.Exceptions
{
    public class BusinessRuleException : BaseException
    {
        public BusinessRuleException(string message) : base(message)
        {

        }

        public BusinessRuleException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public override int ExceptionCode => 400;
    }
}
