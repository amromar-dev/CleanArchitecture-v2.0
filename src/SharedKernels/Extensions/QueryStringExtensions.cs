using System.Globalization;

namespace CleanArchitectureTemplate.SharedKernels.Extensions
{
    public static class QueryStringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> CommaSeparatedToListNumbers(this string str)
        {
            return string.IsNullOrEmpty(str) ? ([]) : str.Split(",").Select(s => int.Parse(s)).ToList();
        }

        /// <summary>
        /// Cast string comma seperated to enum list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="separatedStr"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> CommaSeparatedToListEnums<T>(this string value, string separatedStr = ",") where T : struct, Enum
        {
            if (string.IsNullOrEmpty(value))
                return [];

            var enumList = new List<T>();
            string[] enumValues = value.Split(separatedStr);

            foreach (string enumValue in enumValues)
            {
                if (Enum.TryParse(enumValue, out T parsedEnum))
                    enumList.Add(parsedEnum);
                else
                    throw new ArgumentException($"Invalid {typeof(T).Name} value: {enumValue}");
            }

            return enumList.Distinct().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> CommaSeparatedToListStrings(this string str)
        {
            return string.IsNullOrEmpty(str) ? ([]) : str.Split(",").Select(s => s).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DateTime? ConvertQueryToDateTime(this string query)
        {
            return string.IsNullOrWhiteSpace(query) == false
                ? DateTime.ParseExact(query, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                : null;
        }
    }
}
