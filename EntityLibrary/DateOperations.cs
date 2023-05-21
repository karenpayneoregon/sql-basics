using EntityLibrary.Data;
using EntityLibrary.Models;

namespace EntityLibrary;
public class DateOperations
{
    public static (List<Calendar> list, Exception exception) GetCalendar(int year, int month, bool businessDay, string dow)
    {

        try
        {
            using var context = new Context();

            var list = context.Calendar.Where(calendar =>
                calendar.CalendarYear == year &&
                calendar.CalendarMonth == month &&
                calendar.BusinessDay == businessDay && calendar.DayOfWeekName == dow).ToList();

            return (list, null);
        }
        catch (Exception localException)
        {
            return (null, localException);
        }
    }

    public static (List<Calendar> list, Exception exception) HolidaysInYearBetweenMonths(int year, int startMonth, int endMonth)
    {
        try
        {
            using var context = new Context();

            List<Calendar> list = context.Calendar
                .WhereDatesBetween(startMonth,endMonth)
                .Where(x => x.CalendarYear == year && x.Holiday)
                .ToList();

            return (list, null);
        }
        catch (Exception localException)
        {
            return (null, localException);
        }
    }
}

public static class Extensions
{
    public static IQueryable<Calendar> WhereDatesBetween(this IQueryable<Calendar> sender, int startDate, int endDate)
        => sender.Where(calendar => startDate <= calendar.CalendarMonth && calendar.CalendarMonth <= endDate);
}
