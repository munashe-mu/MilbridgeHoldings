namespace MilbridgeHoldings.Validations.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsPrimitive(this Type t)
        {
            return t.IsPrimitive || t == typeof(decimal) || t == typeof(string) || t == typeof(DateTime);
        }

        public static bool IsHourseWorkedValid(int hoursRequired, int HoursWorkedTotal)
        {
            return hoursRequired == Math.Max(hoursRequired, HoursWorkedTotal);
        }
    }
}
