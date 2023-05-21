namespace WorkingWithDatesSqlServer.Classes;
public sealed class AppOperations
{
    private static readonly Lazy<AppOperations> Lazy = new(() => new());

    public static AppOperations Instance => Lazy.Value;

    public List<string> HolidayColumns => new ()
    { 
        "CalendarYear", "CalendarMonth", "DayOfWeekName", "BusinessDay", "NonBusinessDay", "Weekend", "Weekday", 
        "FirstDateOfWeek", "LastDateOfWeek", "FirstDateOfMonth", "LastDateOfMonth", "FirstDateOfQuarter",
        "FirstDateOfQuarter", "FirstDateOfYear", "LastDateOfYear", "Holiday", "LastDateOfQuarter"
    };
    public List<string> CalendarColumns => new()
    { 
        "CalendarYear", "CalendarMonth", "DayOfWeekName", "BusinessDay",
        "NonBusinessDay", "Weekend", "Weekday", "CalendarDateDescription"
    };
}


