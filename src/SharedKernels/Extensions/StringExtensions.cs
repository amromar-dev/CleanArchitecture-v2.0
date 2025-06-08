using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if the email matches a valid email pattern
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsMatchEmail(this string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Check if the domain matches a valid domain pattern
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool IsMatchDomain(this string domain)
        {
            var pattern = @"^((http[s]?:\/\/)?(www\.)?([a-zA-Z0-9\-_]+\.)+[a-zA-Z]{2,})$";
            return Regex.IsMatch(domain, pattern);
        }

        /// <summary>
        /// Check if the number is valid
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool IsMatchPhone(this string number)
        {
            var pattern = @"^\+\d{1,3}\d{4,14}$";
            return Regex.IsMatch(number, pattern);
        }
    }
}
