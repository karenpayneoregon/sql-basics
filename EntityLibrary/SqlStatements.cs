namespace EntityLibrary;

public class SqlStatements
{
    /// <summary>
    /// Query to get start and end date for the current week where were we ask the server to
    /// which in set in appsettings.json
    /// </summary>
    /// <returns></returns>
    public static string CurrentWeekStartAndEnd() =>
        """
            SELECT DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE)) WeekStart,
                   DATEADD(DAY, 7 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE)) WeekEnd
         """;


}
