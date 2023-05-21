# About

The average developer starting out learning how to interact with an SQL-Server database tends to write their SQL in code using string concatenation. This can lead to issues like SQL injection, mismatched apostrophes and wrong interpretation of dates and time values.

The intent of code presented is to show the proper way to write SQL statements, in this case with dates but also some other things like case statements for instance.

Always write SQL statements in SSMS (SQL-Server Management Studio) and for WHERE conditions create parameters.

Example

```sql
DECLARE @CalendarYear AS INT = 2023;
DECLARE @CalendarMonth AS INT = 5;
DECLARE @BusinessDay AS INT = 1;
DECLARE @DayOfWeekName AS NCHAR(10) = N'Monday';

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
FROM DateTimeDatabase.dbo.Calendar
WHERE CalendarYear = @CalendarYear
      AND CalendarMonth = @CalendarMonth
      AND BusinessDay = @BusinessDay
      AND DayOfWeekName = @DayOfWeekName;
```

In C# Code placing the SQL statement directly with code is fine for short statements while with the example above, place the query in another location or in a separate class as done in the class `SqlStatements`. The actual code is in `SqlOperations` class.


C# code for above SQL

- `SetupLogging.Development()` sets up logging to a file in a folder beneath the main application, LogFiles followed by a folder for the day e.g. 2023-5-17. The code was taken directly from a web project which uses [dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) while here no dependency injection
- `List<Calendar> list = new();` defines what the method returns
- Next line creates an object responsible for connecting to the database, the connection string is in the frontend project in `appsettings.json`
- The next line creates an object to interact with our SQL statement
- The next few lines using `cmd.Parameters.Add` define parameters to the parameters in the SQL statement.

For any code which can fail we wrap in a [try/catch](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements) and on failure write to a log file as per above

- `cn.Open()` opens a connection to the database
- `SqlDataReader? reader = cmd.ExecuteReader();` executes our SQL statement
- `if (reader.HasRows)` returns true if records are returned, false otherwise. There is no `else` in this case as there are since the query was written with known data. For a real application we would include an `else` or the caller of the code can ask for a count of returned data and act accordingly.
- `while (reader.Read())` loops through the returned rows and adds them to the list.

When there are no errors, `return (list, null);` is returned and if there are errors `return (null, localException);` is returned.

The caller then deconstructs what is returned and uses assertion to show data or report an issue.

```csharp
var (data, exception) = SqlOperations.GetCalendar4(2023, 5, 1, DayOfWeek.Monday);

if (exception is null)
{
```

**Backend code**

```csharp
public static (List<Calendar> data, Exception exception) GetCalendar4(int year, int month, int businessDay, DayOfWeek dayOfWeek)
{
    SetupLogging.Development();
    List<Calendar> list = new();

    using var cn = new SqlConnection(ConnectionString());
    using var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.CalendarByYearMonthDay() };

    cmd.Parameters.Add("@CalendarYear", SqlDbType.Int).Value = year;
    cmd.Parameters.Add("@CalendarMonth", SqlDbType.Int).Value = month;
    cmd.Parameters.Add("@BusinessDay", SqlDbType.Int).Value = businessDay;
    cmd.Parameters.Add("@DayOfWeekName", SqlDbType.NVarChar).Value = dayOfWeek.ToString();

    try
    {

        cn.Open();

        var reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {

                list.Add(new Calendar()
                {
                    CalendarDate = reader.GetDateTime(0),
                    CalendarDay = reader.GetInt32(1),
                    FirstDateOfWeek = reader.GetDateTime(2),
                    LastDateOfWeek = reader.GetDateTime(3),
                    FirstDateOfMonth = reader.GetDateTime(4),
                    LastDateOfMonth = reader.GetDateTime(5),
                    FirstDateOfQuarter = reader.GetDateTime(6),
                    LastDateOfQuarter = reader.GetDateTime(7),
                    FirstDateOfYear = reader.GetDateTime(8),
                    LastDateOfYear = reader.GetDateTime(9),
                    Holiday = reader.GetString(10)
                });
            }

        }

        return (list, null);

    }
    catch (Exception localException)
    {
        Log.Error(localException, "Failed reading data");
        
        return (null, localException);
    }

}
```

## Various SQL statements

Open SSMS, select `DateTimeDatabase` for the first statement, the others do not need a specific database as they work off any database.

```sql
SELECT id,
       FirstName + ' ' + LastName 'full name',
       FORMAT(CAST(StartTime AS DATETIME), 'hh:mm tt') 'StartTime formatted',
       StartTime
FROM DateTimeDatabase.dbo.TimeTable;

SELECT EventID,
       StartDate,
       CONVERT(VARCHAR(10), StartDate, 103) AS startDateShort
FROM DateTimeDatabase.dbo.Events;

SELECT DATEADD(DAY, -1, CAST(GETDATE() AS DATE)) AS YesterdayDate;
SELECT DATEADD(DAY, -4, CAST(GETDATE() AS DATE)) AS FourDaysAgo;
SELECT DATEADD(DAY, 1, CAST(GETDATE() AS DATE)) AS TomorrowDate;
SELECT DATEADD(MONTH, -5, CAST(GETDATE() AS DATE)) AS fiveMonthsAgo;

--- Get month number
SELECT MONTH('August' + '1,1');
```


## By Culture

```sql
DECLARE @date datetime2(7);
SET @date = '2023-08-07 19:14:02.080';
SELECT 
  FORMAT(@date, 'd', 'en-us') AS 'US English',
  FORMAT(@date, 'd', 'en-gb') AS 'British',
  FORMAT(@date, 'd', 'de-de') AS 'German',
  FORMAT(@date, 'd', 'jp-jp') AS 'Japanese',
  FORMAT(@date, 'd', 'ru') AS 'Russia',
  FORMAT(@date, 'd', 'vi') AS 'Vietnam'
```