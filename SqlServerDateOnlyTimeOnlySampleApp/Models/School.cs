namespace SqlServerDateOnlyTimeOnlySampleApp.Models;
public class School
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly Founded { get; set; }
    public DateTime LastVisited { get; set; }
    public TimeSpan LegacyTime { get; set; }
    public List<Term> Terms { get; } = new();
    public List<OpeningHours> OpeningHours { get; } = new();
}