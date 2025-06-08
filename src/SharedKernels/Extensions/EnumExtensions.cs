using CleanArchitectureTemplate.SharedKernels.Attributes;
using CleanArchitectureTemplate.SharedKernels.Environments;

namespace CleanArchitectureTemplate.SharedKernels.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field?.GetCustomAttributes(typeof(StringValueAttribute), false)
                                  .Cast<StringValueAttribute>();

            return attributes?.FirstOrDefault(a => a.Client == ApplicationEnvironment.CurrentClient)?.Value
                ?? attributes?.FirstOrDefault(a => a.Client == ApplicationEnvironmentClient.Default)?.Value
                ?? value.ToString();
        }

        public static TEnum? FromStringValue<TEnum>(this string stringValue)
            where TEnum : struct, Enum
        {
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attributes = field.GetCustomAttributes(typeof(StringValueAttribute), false)
                                      .Cast<StringValueAttribute>();

                var match = attributes.FirstOrDefault(a => a.Client == ApplicationEnvironment.CurrentClient && a.Value.Equals(stringValue, StringComparison.OrdinalIgnoreCase))
                         ?? attributes.FirstOrDefault(a => a.Client == ApplicationEnvironmentClient.Default && a.Value.Equals(stringValue, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                    return (TEnum)field.GetValue(null);
            }

            return null;
        }
    }

}
