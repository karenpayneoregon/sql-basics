namespace DapperForAccessDatabaseMdb.Classes;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
    public int SomeNumber { get; set; }
    public override string ToString() => $"{Id,-4}{FirstName,-5}{LastName,-15}{BirthDate,-15:d}{Active,-4}{SomeNumber}";
}
