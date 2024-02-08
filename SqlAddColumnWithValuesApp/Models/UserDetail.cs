namespace SqlAddColumnWithValuesApp.Models;
public class UserDetail
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public int? Pin { get; set; }
    public bool? ActiveMember { get; set; }
    public DateOnly JoinDate { get; set; }
    public string PhoneNumber { get; set; }
}