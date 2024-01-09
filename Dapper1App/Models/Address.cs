namespace Dapper1App.Models;

public class Address
{
    public int Id { get; set; }
    public int ContactId { get; set; }
    public string AddressType { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public int StateId { get; set; }
    public string PostalCode { get; set; }
    internal bool IsNew => (this.Id == default(int));
    public bool IsDeleted { get; set; }
}