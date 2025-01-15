namespace ProductsCategoriesApp1.Models;

/// <summary>
/// Represents a customer entity with details such as company information, contact details, 
/// address, and associated country and contact type.
/// </summary>
/// <remarks>
/// This class is used to model customer data, including relationships to associated entities 
/// like <see cref="Contact"/>, <see cref="Country"/>, and <see cref="ContactType"/>.
/// </remarks>
public class Customer
{
    public int CustomerIdentifier { get; set; }
    public string CompanyName { get; set; }
    public int ContactId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public int CountryIdentifier { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string Region { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int ContactTypeIdentifier { get; set; }
    public Contact Contact { get; set; }
    public Country Country { get; set; }
    public ContactType ContactType { get; set; }
}