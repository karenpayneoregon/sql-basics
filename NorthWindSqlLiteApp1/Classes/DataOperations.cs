using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using NorthWindSqlLiteApp1.Data;

using static NorthWindSqlLiteApp1.Classes.Core.SpectreConsoleHelpers;

namespace NorthWindSqlLiteApp1.Classes;
internal class DataOperations
{
    public static void UpdateCustomerById()
    {

        PrintPink();
        
        using (var context = new Context())
        {
            var customer = context.Customers.AsNoTracking().FirstOrDefault(x => x.CustomerIdentifier == 3);
            if (customer != null)
            {
                customer.CompanyName = "Updated Company Name";
                context.Attach(customer).State = EntityState.Modified;
                var result = context.SaveChanges();
                if (result == 1)
                {
                    SuccessPill(Justify.Left, $"Customer '{customer.CompanyName}' updated successfully.");
                }
                else
                {
                    ErrorPill(Justify.Left, $"Customer with ID {customer.CustomerIdentifier} was not updated.");
                }
            }
            else
            {
                ErrorPill(Justify.Left, "Customer not found with the specified ID.");
            }
        }

        Console.WriteLine();
        
    }

    public static void DisplayTop5Customers()
    {
        
        PrintPink();
        
        using (var context = new Context())
        {
            var customers = context.Customers.AsNoTracking().Take(5).ToList();
            customers.ForEach(c => Console.WriteLine($"CustomerID: {c.CustomerIdentifier}, CompanyName: {c.CompanyName}"));
        }

        Console.WriteLine();
        
    }

    public static void GetCustomersCount()
    {

        PrintPink();
        
        using (var context = new Context())
        {
            var count = context.Customers.AsNoTracking().Count();
            PanelDisplay("Total Customers", $"Count: {count}");
        }

        Console.WriteLine();
        
    }
}
