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

public partial class ContactDevices 
{
    public int DeviceId { get; set; }
    public int? ContactId { get; set; }
    public int? PhoneTypeIdentifier { get; set; }
    public string PhoneNumber { get; set; }
    public Contact Contact { get; set; }
    //public PhoneType PhoneTypeIdentifierNavigation { get; set; }
}