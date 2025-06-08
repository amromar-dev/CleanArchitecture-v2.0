using System.Reflection;

namespace CleanArchitectureTemplate.API.DependencyInjections.Extensions
{
    /// <summary>
    /// Area configurations names
    /// </summary>
    public static class AreaExtension
    {
        /// <summary>
        /// 
        /// </summary>
        public const string IdentityArea = "Identity";

        /// <summary>
        /// 
        /// </summary>
        public const string SampleArea = "SampleArea";

        /// <summary>
        /// Get all the areas defined in the area configuration class
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAreas()
        {
            var fields = typeof(AreaExtension).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return fields.Where(f => f.IsLiteral && !f.IsInitOnly).Select(f => f.GetValue(null) as string).Where(v => !string.IsNullOrEmpty(v)).ToList();
        }
    }
}
