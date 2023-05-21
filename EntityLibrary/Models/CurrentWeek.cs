
namespace EntityLibrary.Models;
public class CurrentWeek
{
    public DateTime WeekStart { get; set; }
    public DateTime WeekEnd { get; set; }
    public string Display 
        => $"Start: {DateOnly.FromDateTime(WeekStart)} End: {DateOnly.FromDateTime(WeekEnd)}";
}
