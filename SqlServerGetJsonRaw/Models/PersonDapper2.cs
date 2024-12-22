#nullable disable
namespace SqlServerGetJsonRaw.Models;

public class PersonDapper2
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string AddressType { get; set; }
    public string AddressJson { get; set; }

    public List<Address> Addresses { get; set; }
    public override string ToString() => $"{FirstName} {LastName}";

}