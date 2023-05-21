namespace GlobalConnection.Models;
public class Contact
{
    public int ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ContactTypeIdentifier { get; set; }
    public override string ToString() => LastName;
}
