using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Classes;
internal class EntityOperations
{
    public static List<Customers> CustomersList()
    {
        using var context = new Context();
        return context.Customers
            .Include(x => x.Contact)
            //.ThenInclude(x => x.ContactDevices)
            .Include(x => x.ContactTypeIdentifierNavigation)
            .Include(x => x.CountryIdentifierNavigation)
            .ToList();
    }
}
