namespace NorthCustomersToolGenerated.Models;

public partial class ContactDevices 
{
    public int DeviceId { get; set; }
    public int? ContactId { get; set; }
    public int? PhoneTypeIdentifier { get; set; }
    public string PhoneNumber { get; set; }
    public Contact Contact { get; set; }
    //public PhoneType PhoneTypeIdentifierNavigation { get; set; }
}