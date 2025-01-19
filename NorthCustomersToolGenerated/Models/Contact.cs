namespace NorthCustomersToolGenerated.Models;

public class Contact
{
    public int ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ContactTypeIdentifier { get; set; }

    public ContactType ContactTypeIdentifierNavigation { get; set; }
    public ICollection<ContactDevices> ContactDevices { get; } = new List<ContactDevices>();
}