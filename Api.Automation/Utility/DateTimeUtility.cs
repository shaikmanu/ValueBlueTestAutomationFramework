namespace Api.Automation.Tests.Utility
{
    public class DateTimeUtility
    {
        public static DateTime TruncateToPrecision(DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Kind);
        }

        public static void AssertDateTimeCloseTo(DateTime actual, DateTime expected, TimeSpan tolerance)
        {
            DateTime truncatedActual = TruncateToPrecision(actual);
            DateTime truncatedExpected = TruncateToPrecision(expected);

            if (Math.Abs((truncatedActual - truncatedExpected).TotalSeconds) > tolerance.TotalSeconds)
            {
                throw new InvalidOperationException($"Expected date time to be close to {truncatedExpected}, but was {truncatedActual}");
            }
        }
    }
}
