
using ConsoleApp1;
using ConsoleApp1.Classes;
using ConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;

//using Karen = (string test, int test1);


namespace ConsoleApp1;


internal class Program
{
    static void Main(string[] args)
    {

        DapperOperations operations = new ();
        var list = operations.GetAll();

        Console.WriteLine("Hello, World!");
    }

    private static void EntityFrameworkCore()
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

