using EntityFrameworkCoreSqlServer.Classes;
using EntityLibrary;
using EntityLibrary.Data;
using EntityLibrary.Models;
using Microsoft.EntityFrameworkCore;
using static EntityLibrary.SqlStatements;

namespace EntityFrameworkCoreSqlServer;

internal partial class Program
{
    /// <summary>
    /// Here we are doing the same sql statement as in DataLibrary.SqlOperations.GetCalendar4 but
    /// with an ORM EF Core 7
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        //WeekStartEnd();
        Example1();
        AnsiConsole.MarkupLine("[yellow]Done[/]");
        Console.ReadLine();
    }
    /// <summary>
    /// Get records that are in the year 2023, month May, is business day and Monday
    /// This mirrors the query in DataLibrary
    /// </summary>
    private static void Example1()
    {
        int year = 2023;
        int month = 5;
        bool businessDay = true;
        string dow = DayOfWeek.Monday.ToString();

        using var context = new Context();

        var (list, exception) = DateOperations.GetCalendar(year, month, businessDay, dow);

        if (exception is null)
        {
            foreach (var cal in list)
            {
                Console.WriteLine($"{cal.CalendarDate,-10:MM/dd/yyyy} {cal.CalendarDay,3} {cal.Holiday.ToYesNo(),-4}");
            }
        }
        else
        {
            AnsiConsole.WriteException(exception);
        }
    }

    /// <summary>
    /// Get records for the year 2023, months between January and June
    /// </summary>
    private static void Example2()
    {
        int year = 2023;
        int startMonth = 1;
        int endMonth = 6;

        var (list, exception) = DateOperations.HolidaysInYearBetweenMonths(year, startMonth, endMonth);
        if (exception is null)
        {
            foreach (var cal in list)
            {
                Console.WriteLine($"{cal.CalendarDate,-10:MM/dd/yyyy} {cal.CalendarDay,-3} {cal.Description}");
            }
        }
        else
        {
            AnsiConsole.WriteException(exception);
        }
    }

    /// <summary>
    /// This is an oddball operation as EF Core works off tables in a database, here there is no
    /// table so we first create a model then tell EF Core to ignore it by indicating the DbSet is
    /// keyless via HasNoKey in the DbContext.
    ///
    /// FromSqlRaw indicates the return type which we can leave out yet it suits for readability for new to C#
    /// </summary>
    private static void WeekStartEnd()
    {
        using var context = new Context();

        CurrentWeek results = context.CurrentWeeks
            .FromSqlRaw<CurrentWeek>(CurrentWeekStartAndEnd()).FirstOrDefault();

        Console.WriteLine(results!.Display);
    }
}