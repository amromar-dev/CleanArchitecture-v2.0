namespace CleanArchitectureTemplate.SharedKernels.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime birthDate)
        {
            try
            {
                DateTime today = DateTime.Today;
                int age = today.Year - birthDate.Year;

                if (birthDate.Date > today.AddYears(-age))
                    age--;

                return age;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
