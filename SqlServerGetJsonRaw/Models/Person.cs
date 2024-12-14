namespace SqlServerGetJsonRaw.Models;
#nullable disable
/// <summary>
/// Represents a person with details such as name, date of birth, and address information.
/// </summary>
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Company { get; set; }
    public override string ToString() => $"{FirstName} {LastName}";

}