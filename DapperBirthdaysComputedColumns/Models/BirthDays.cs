namespace DapperBirthdaysComputedColumns.Models;
internal class BirthDays
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public int YearsOld { get; set; }
}
