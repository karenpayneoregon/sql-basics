namespace NorthWindSqlLiteApp1.Models.Projections;

/// <summary>
/// Represents a projection of customer and contact information.
/// </summary>
/// <remarks>
/// This class encapsulates details about a customer, including their identifier, company name,
/// contact person's first and last names, contact title, and a combined name representation.
/// </remarks>
public class CustomerWithContact
{
    public int CustomerIdentifier { get; }
    public string CompanyName { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string ContactTitle { get; }
    public string Name { get; }

    public CustomerWithContact(int customerIdentifier, string companyName, string firstName, string lastName, string contactTitle, string name)
    {
        CustomerIdentifier = customerIdentifier;
        CompanyName = companyName;
        FirstName = firstName;
        LastName = lastName;
        ContactTitle = contactTitle;
        Name = name;
    }

    public override bool Equals(object? value) 
        => value is CustomerWithContact other && 
           EqualityComparer<int>.Default.Equals(other.CustomerIdentifier, CustomerIdentifier) && EqualityComparer<string>.Default.Equals(other.CompanyName, CompanyName) && EqualityComparer<string>.Default.Equals(other.FirstName, FirstName) && EqualityComparer<string>.Default.Equals(other.LastName, LastName) && EqualityComparer<string>.Default.Equals(other.ContactTitle, ContactTitle) && EqualityComparer<string>.Default.Equals(other.Name, Name);

    public override int GetHashCode()
    {
        var hash = 0x7a2f0b42;
        hash = -1521134295 * hash + EqualityComparer<int>.Default.GetHashCode(CustomerIdentifier);
        hash = -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(CompanyName);
        hash = -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(FirstName);
        hash = -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(LastName);
        hash = -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(ContactTitle);
        return -1521134295 * hash + EqualityComparer<string>.Default.GetHashCode(Name);
    }
}