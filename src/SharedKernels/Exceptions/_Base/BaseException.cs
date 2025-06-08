namespace CleanArchitectureTemplate.SharedKernels.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public abstract int ExceptionCode { get; }
    }
}
