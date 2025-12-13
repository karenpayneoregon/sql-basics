namespace DeleteDuplicateRowsSqlServerTable.Models;

public class PersonWithDuplicates
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthDay { get; set; }
}