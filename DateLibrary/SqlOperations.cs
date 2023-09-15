using System.Data;
using System.Diagnostics;
using System.Text;
using Dapper;
using DateLibrary.Models;
using Microsoft.Data.SqlClient;
using Serilog;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

#pragma warning disable CS8619

namespace DateLibrary;

/// <summary>
/// Code samples for working with SQL-Server
/// Uses
/// - SqlClient data provider
///   https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-7.0
/// - Dapper ORM
///   https://www.learndapper.com/
///
/// The goal here is to get familiar with writing code to interact with a database, not the data provider
/// </summary>
public class SqlOperations
{
    /// <summary>
    /// First iteration to test the waters
    /// * No exception handling
    /// * Static values for WHERE
    /// </summary>
    /// <returns>empty list</returns>
    public static List<Calendar> GetCalendar1()
    {
        List<Calendar> list = new();

        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.CalendarByYearMonthDay()};

        cmd.Parameters.Add("@CalendarYear", SqlDbType.Int).Value = 2023;
        cmd.Parameters.Add("@CalendarMonth", SqlDbType.Int).Value = 5;
        cmd.Parameters.Add("@BusinessDay", SqlDbType.Int).Value = 1;
        cmd.Parameters.Add("@DayOfWeekName", SqlDbType.NVarChar).Value = "Monday";

        cn.Open();

        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Debug.WriteLine(reader.GetInt32(1));
            }
            
        }

        return list;

    }

    /// <summary>
    /// Variation of a above, set default argument values
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="businessDay"></param>
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
    public static List<Calendar> GetCalendar2(int year = 2023, int month = 5, int businessDay = 1, string dayOfWeek = "Monday")
    {
        List<Calendar> list = new();

        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.CalendarByYearMonthDay() };

        cmd.Parameters.Add("@CalendarYear", SqlDbType.Int).Value = year;
        cmd.Parameters.Add("@CalendarMonth", SqlDbType.Int).Value = month;
        cmd.Parameters.Add("@BusinessDay", SqlDbType.Int).Value = businessDay;
        cmd.Parameters.Add("@DayOfWeekName", SqlDbType.NVarChar).Value = dayOfWeek;

        cn.Open();

        var reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Debug.WriteLine(reader.GetInt32(1));
            }

        }

        return list;

    }

    /// <summary>
    /// Variation of above, no default value, uses <see cref="DayOfWeek"/> so we have no typos
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="businessDay"></param>s
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
    public static List<Calendar> GetCalendar3(int year, int month, int businessDay, DayOfWeek dayOfWeek )
    {
        List<Calendar> list = new();

        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.CalendarByYearMonthDay() };

        cmd.Parameters.Add("@CalendarYear", SqlDbType.Int).Value = year;
        cmd.Parameters.Add("@CalendarMonth", SqlDbType.Int).Value = month;
        cmd.Parameters.Add("@BusinessDay", SqlDbType.Int).Value = businessDay;
        cmd.Parameters.Add("@DayOfWeekName", SqlDbType.NVarChar).Value = dayOfWeek.ToString();

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
                    LastDateOfYear =reader.GetDateTime(9),
                    Holiday = reader.GetString(10)
                });
            }
        }

        return list;

    }
    /// <summary>
    /// Variation of above
    /// * Has exception handling
    /// * Returns a list and an exception
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="businessDay"></param>
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
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

            SqlDataReader? reader = cmd.ExecuteReader();

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
    /// <summary>
    /// Ask out SQL-Server for start and end dates for the current week
    /// </summary>
    /// <returns>two <see cref="DateOnly"/> values</returns>
    /// <remarks>
    /// In prior years we would use <see cref="DateTime"/> and format to a short date string
    /// </remarks>
    public static (DateOnly startDate, DateOnly endDate) GetCurrentWeekStartEnd()
    {
        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand { Connection = cn, CommandText = SqlStatements.CurrentWeekStartAndEnd() };

        cn.Open();

        var reader = cmd.ExecuteReader();
        reader.Read();

        return (reader.GetFieldValue<DateOnly>(0), 
                reader.GetFieldValue<DateOnly>(1));

    }

    /// <summary>
    /// For completeness there is a free open-source object-relational mapping (ORM) library for .NET and .NET Core applications.
    /// which typically requires less code than the convention method as in all methods above.
    ///
    /// Downside here is Dapper does not support <see cref="DateOnly"/> so we must convert from <see cref="DateTime"/> to
    /// <see cref="DateOnly"/> in the return statement.
    /// </summary>
    /// <returns></returns>
    public static (DateOnly startDate, DateOnly endDate) GetCurrentWeekStartEndDapper()
    {
        using var cn = new SqlConnection(ConnectionString());
        var result = cn.Query<CurrentWeek>(SqlStatements.CurrentWeekStartAndEnd()).FirstOrDefault()!;
        return (DateOnly.FromDateTime(result.WeekStart), DateOnly.FromDateTime(result.WeekEnd));
    }

    public static List<Calendar> HolidaysDuringTheWeekForYear(int year, bool isHoliday, bool isBusinessDay)
    {
        using var cn = new SqlConnection(ConnectionString());
        var parameters = new
        {
            @CalendarYear = year, 
            @Holiday = isHoliday, 
            @BusinessDay = isBusinessDay
        };
        return cn.Query<Calendar>(SqlStatements.HolidaysDuringTheWeekForYear(), parameters).ToList();
    }

    public static List<Calendar> HolidaysInYearBetweenMonths(int year, int startMonth, int endMonth)
    {
        using var cn = new SqlConnection(ConnectionString());
        var parameters = new
        {
            @CalendarYear = year, 
            @StartMonth = startMonth, 
            @EndMonth = endMonth
        };
        return cn.Query<Calendar>(
            SqlStatements.HolidaysInYearBetweenMonths(), 
            parameters).ToList();
    }

    public static string GetPeoplesAges()
    {
        using var cn = new SqlConnection(ConnectionString());
        List<Person> list = cn.Query <Person>(SqlStatements.PeopleAge()).ToList();
        StringBuilder builder  = new ();
        foreach (Person person in list)
        {
            builder.AppendLine(person.Display);
        }

        return builder.ToString();
    }
}
