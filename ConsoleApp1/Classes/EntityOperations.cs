using ConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Classes;
internal class EntityOperations
{
    public static void EntityFrameworkCore()
    {
        using var context = new Context();
        var customers = context.Customers
            .Include(x => x.Contact)
            .ThenInclude(x => x.ContactDevices)
            .Include(x => x.ContactTypeIdentifierNavigation)
            .Include(x => x.CountryIdentifierNavigation)
            .Where(x => x.Contact.ContactTypeIdentifierNavigation.ContactTitle.Contains("ne"))
            .ToList();
    }
}
