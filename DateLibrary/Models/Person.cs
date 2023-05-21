
#pragma warning disable CS8618
namespace DateLibrary.Models;
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public int Age { get; set; }
    public string Display => $"{FirstName} {LastName} {Age} {PhoneNumber}";
    public override string ToString() => $"{FirstName} {LastName} {Age}";


}
