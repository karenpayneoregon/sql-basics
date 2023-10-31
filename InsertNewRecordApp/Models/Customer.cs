using Dapper.Contrib.Extensions;

namespace InsertNewRecordApp.Models;

[Table("Customer")]
public class Customer
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Active { get; set; }
    public override string ToString() => $"{FirstName} {LastName}";

}