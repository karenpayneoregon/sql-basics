#nullable disable
namespace DateLibrary.Models;

public partial class Calendar
{
    public DateTime CalendarDate { get; set; }

    public int CalendarYear { get; set; }

    public int CalendarMonth { get; set; }

    public int CalendarDay { get; set; }

    public string DayOfWeekName { get; set; }

    public DateTime FirstDateOfWeek { get; set; }

    public DateTime LastDateOfWeek { get; set; }

    public DateTime FirstDateOfMonth { get; set; }

    public DateTime LastDateOfMonth { get; set; }

    public DateTime FirstDateOfQuarter { get; set; }

    public DateTime LastDateOfQuarter { get; set; }

    public DateTime FirstDateOfYear { get; set; }

    public DateTime LastDateOfYear { get; set; }

    public bool BusinessDay { get; set; }

    public bool NonBusinessDay { get; set; }

    public bool Weekend { get; set; }

    public string Holiday { get; set; }

    public bool Weekday { get; set; }

    public string CalendarDateDescription { get; set; }
    public override string ToString() => CalendarDate.ToShortDateString();

}