namespace DateLibrary;

/// <summary>
/// Each SQL statement was created in SSMS, pasted in below and used.
/// The syntax for creating strings is known as raw string literals
/// </summary>
public class SqlStatements
{
    /// <summary>
    /// Select records from DateTimeDatabase.dbo.Calendar WHERE
    /// Specific Year, Month, is business day and specific day of week
    /// </summary>
    /// <returns></returns>
    public static string CalendarByYearMonthDay() =>
        """
            SELECT CalendarDate,
                   CalendarDay,
                   FirstDateOfWeek,
                   LastDateOfWeek,
                   FirstDateOfMonth,
                   LastDateOfMonth,
                   FirstDateOfQuarter,
                   LastDateOfQuarter,
                   FirstDateOfYear,
                   LastDateOfYear,
                   CASE
                       WHEN Holiday = 1 THEN
                           'Yes'
                       ELSE
                           'No'
                   END AS Holiday
            FROM dbo.Calendar
            WHERE CalendarYear = @CalendarYear
                  AND CalendarMonth = @CalendarMonth
                  AND BusinessDay = @BusinessDay
                  AND DayOfWeekName = @DayOfWeekName;
            """;

    public static string CalendarByYearMonthDayComputedHoliday() =>
        """
        SELECT CalendarDate,
               CalendarDay,
               FirstDateOfWeek,
               LastDateOfWeek,
               FirstDateOfMonth,
               LastDateOfMonth,
               FirstDateOfQuarter,
               LastDateOfQuarter,
               FirstDateOfYear,
               LastDateOfYear,
               [HolidayComputed] AS Holiday
        FROM dbo.Calendar
        WHERE CalendarYear = @CalendarYear
              AND CalendarMonth = @CalendarMonth
              AND BusinessDay = @BusinessDay
              AND DayOfWeekName = @DayOfWeekName;
        """;

    /// <summary>
    /// Query to get start and end date for the current week where were we ask the server to
    /// which in set in appsettings.json
    /// </summary>
    /// <returns></returns>
    public static string CurrentWeekStartAndEnd() =>
        """
            SELECT DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE)) WeekStart,
                   DATEADD(DAY, 7 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE)) WeekEnd;
         """;

    public static string HolidaysDuringTheWeekForYear() =>
        """
            SELECT CalendarDate,
                   CalendarYear,
                   CalendarMonth,
                   CalendarDay,
                   DayOfWeekName,
                   FirstDateOfWeek,
                   LastDateOfWeek,
                   FirstDateOfMonth,
                   LastDateOfMonth,
                   FirstDateOfQuarter,
                   LastDateOfQuarter,
                   FirstDateOfYear,
                   LastDateOfYear,
                   BusinessDay,
                   NonBusinessDay,
                   Weekend,
                   Holiday,
                   [Weekday],
                   CalendarDateDescription
            FROM DateTimeDatabase.dbo.Calendar
            WHERE CalendarYear = @CalendarYear
                  AND Holiday = @Holiday
                  AND BusinessDay = @BusinessDay;
         """;

    public static string HolidaysInYearBetweenMonths() =>
        """
         SELECT CalendarDate,
                CalendarYear,
                CalendarMonth,
                CalendarDay,
                DayOfWeekName,
                BusinessDay,
                NonBusinessDay,
                Weekend,
                [Weekday],
                CalendarDateDescription
         FROM DateTimeDatabase.dbo.Calendar
         WHERE CalendarYear = @CalendarYear AND 
            CalendarMonth BETWEEN @StartMonth AND @EndMonth AND Holiday = 1
         """;

    public static string PeopleAge() =>
        """
         SELECT Id,
                FirstName,
                LastName,
                CASE
                    WHEN Gender = 1 THEN
                        'Male'
                    ELSE
                        'Female'
                END AS Gender,
                FORMAT(BirthDay, 'MM/dd/yyyy') AS BirthDay,
                CONVERT(INT, ROUND(DATEDIFF(HOUR, BirthDay, GETDATE()) / 8766.0, 0)) AS Age,
                SUBSTRING(PhoneNumber, 1, 3) + '-' + SUBSTRING(PhoneNumber, 4, 3) + '-' + SUBSTRING(PhoneNumber, 7, 4) AS PhoneNumber
         FROM dbo.People;
         """;

    public static string GetProductByName() =>
        """
        SELECT ProductID,
               DiscontinuedDate
        FROM dbo.Products
        WHERE ProductName = @ProductName;
        """;


}
