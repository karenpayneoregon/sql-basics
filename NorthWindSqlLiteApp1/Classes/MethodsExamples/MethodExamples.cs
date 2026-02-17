using NorthWindSqlLiteApp1.Classes.Core;
using Spectre.Console;
using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Models;
using Serilog;
using static NorthWindSqlLiteApp1.Classes.Core.SpectreConsoleHelpers;

namespace NorthWindSqlLiteApp1.Classes.MethodsExamples;
public class MethodExamples
{
    public static void AddCustomer1()
    {

        PrintPink();

        using (var context = new Context())
        {
            var newCustomer = new Customers
            {
                CompanyName = "New Customer",
                Street = "123 New St",
                City = "New City",
                PostalCode = "12345",
                Phone = "555-1234",
                CountryIdentifier = 20,
                ContactTypeIdentifier = 7,
                ContactId = 5
            };

            context.Customers.Add(newCustomer);

            var result = context.SaveChanges();

            if (result == 1)
            {
                SuccessPill(Justify.Left, $"Customer added successfully with ID {newCustomer.CustomerIdentifier}.");
            }
            else
            {
                ErrorPill(Justify.Left, "Failed to add the new customer.");
            }
        }
        Console.WriteLine();
    }

    public static bool AddCustomer2()
    {

        using (var context = new Context())
        {
            var newCustomer = new Customers
            {
                CompanyName = "New Customer",
                Street = "123 New St",
                City = "New City",
                PostalCode = "12345",
                Phone = "555-1234",
                CountryIdentifier = 20,
                ContactTypeIdentifier = 7,
                ContactId = 5
            };

            context.Customers.Add(newCustomer);

            return context.SaveChanges() == 1;

        }
    }

    public static (bool success, int identifier) AddCustomer3()
    {

        using (var context = new Context())
        {
            var newCustomer = new Customers
            {
                CompanyName = "New Customer",
                Street = "123 New St",
                City = "New City",
                PostalCode = "12345",
                Phone = "555-1234",
                CountryIdentifier = 20,
                ContactTypeIdentifier = 7,
                ContactId = 5
            };

            context.Customers.Add(newCustomer);

            var result = context.SaveChanges();

            return (result == 1, newCustomer.CustomerIdentifier);
        }
    }

    public static (bool success, int identifier, Exception error) AddCustomer4()
    {

        using (var context = new Context())
        {
            var newCustomer = new Customers
            {
                CompanyName = "New Customer",
                Street = "123 New St",
                City = "New City",
                PostalCode = "12345",
                Phone = "555-1234",
                CountryIdentifier = 20,
                ContactTypeIdentifier = 7,
                ContactId = 5
            };

            context.Customers.Add(newCustomer);

            try
            {
                var result = context.SaveChanges();

                return (result == 1, newCustomer.CustomerIdentifier, null)!;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while adding customer.");
                return (false, 0, ex);
            }
        }
    }
}