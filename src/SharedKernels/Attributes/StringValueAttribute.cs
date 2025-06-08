using CleanArchitectureTemplate.SharedKernels.Environments;

namespace CleanArchitectureTemplate.SharedKernels.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StringValueAttribute(string value, ApplicationEnvironmentClient client = ApplicationEnvironmentClient.Default) : Attribute
    {
        public string Value { get; } = value;

        public ApplicationEnvironmentClient Client { get; } = client;
    }
}
