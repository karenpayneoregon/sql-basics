namespace DapperForAccessDatabase.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool Active { get; set; }
    public override string ToString() => $"{Id,-4}{FirstName,-5}{LastName,-15}{BirthDate,-15:d}{Active}";

}
