namespace HistoryMaps.BlazorApp.Extensions;

public static class IntExtensions
{
    public static string ToYearString(this int year, bool addG = false, bool addOurAge = false)
    {
        return year < 0 
            ? -year + (addG ? "г" : "") + " до н.э." 
            : year + (addG ? "г" : "") + (addOurAge ? " н.э." : "");
    }
}