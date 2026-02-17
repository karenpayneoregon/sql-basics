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
    /// <summary>
    /// Filters a sequence of <see cref="Calendar"/> objects to include only those whose 
    /// <see cref="Calendar.CalendarMonth"/> falls within the specified range.
    /// </summary>
    /// <param name="sender">The source sequence of <see cref="Calendar"/> objects.</param>
    /// <param name="startDate">The starting month of the range (inclusive).</param>
    /// <param name="endDate">The ending month of the range (inclusive).</param>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> containing the <see cref="Calendar"/> objects 
    /// that satisfy the specified date range condition.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="sender"/> is <c>null</c>.
    /// </exception>
    public static IQueryable<Calendar> WhereDatesBetween(this IQueryable<Calendar> sender, int startDate, int endDate)
        => sender.Where(calendar => startDate <= calendar.CalendarMonth && calendar.CalendarMonth <= endDate);
}
