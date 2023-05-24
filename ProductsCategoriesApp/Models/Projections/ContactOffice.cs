using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCategoriesApp.Models.Projections;
public class ContactOffice
{
    [Key]
    public int ContactId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? ContactTypeIdentifier { get; set; }
    [InverseProperty("Contact")]
    public virtual ContactDevices OfficeDevice { get; init; } = new();

    [ForeignKey("ContactTypeIdentifier")]
    [InverseProperty("Contacts")]
    public virtual ContactType ContactTypeIdentifierNavigation { get; set; }
    public static Expression<Func<Contacts, ContactOffice>> Projection
    {
        get
        {

            return (contact) => new ContactOffice()
            {
                ContactId = contact.ContactId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                ContactTypeIdentifier = contact.ContactTypeIdentifier,
                OfficeDevice = contact.ContactDevices.FirstOrDefault(),
                ContactTypeIdentifierNavigation = contact.ContactTypeIdentifierNavigation
            };
        }
    }
}
