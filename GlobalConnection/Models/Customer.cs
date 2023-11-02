namespace GlobalConnection.Models;
/// <summary>
/// Represents a customer entity with properties such as customer identifier, company name, contact ID, street, city, postal code, country identifier, contact title, first name, and last name.
/// </summary>
public class Customer
{
    public int CustomerIdentifier { get; set; }
    public string CompanyName { get; set; }
    public int ContactId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public int CountryIdentifier { get; set; }
    public string ContactTitle { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public override string ToString() => CompanyName;

}
