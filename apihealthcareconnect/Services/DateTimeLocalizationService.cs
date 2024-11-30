namespace apihealthcareconnect.Services
{
    public static class DateTimeLocalizationService
    {
        public static DateTime ToBrazilTime(this DateTime dateTime)
        {
            TimeZoneInfo brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), brasilTimeZone);
        }
    }
}
