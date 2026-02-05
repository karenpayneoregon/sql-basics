using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using NorthWindSqlLiteApp1.Data;

using static NorthWindSqlLiteApp1.Classes.Core.SpectreConsoleHelpers;

namespace NorthWindSqlLiteApp1.Classes;
/// <summary>
/// Provides data-related operations for interacting with the NorthWind SQLite database.
/// </summary>
/// <remarks>
/// This class contains methods for performing various database operations, such as:
/// - Displaying top customers
/// - Retrieving customer details with related data
/// - Counting total customers
/// - Updating customer information
/// 
/// It utilizes Entity Framework Core for database interactions and includes helper methods 
/// for displaying formatted output using Spectre.Console.
/// </remarks>
internal class DataOperations
{
    /// <summary>
    /// Displays the top 5 customers from the database.
    /// </summary>
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

    /// <summary>
    /// Retrieves a customer with related data using multiple joins.
    /// </summary>
    /// <remarks>
    /// This method fetches a customer with a specific identifier and includes related entities such as:
    /// - Contact information
    /// - Contact type
    /// - Country details
    /// - Orders and their details
    /// - Products and their suppliers
    /// - Supplier's country details
    /// </remarks>
    public static void CustomerWithJoins()
    {

        PrintPink();

        using (var context = new Context())
        {
            var customer = context
                .Customers
                .AsNoTracking()
                .Include(x => x.Contact)
                .Include(x => x.ContactTypeIdentifierNavigation)
                .Include(x => x.CountryIdentifierNavigation)
                .Include(x => x.Orders)
                .ThenInclude(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Supplier)
                .ThenInclude(x => x.CountryIdentifierNavigation)
                .FirstOrDefault(x => x.CustomerIdentifier == 1);
        }

        Console.WriteLine();

    }

    /// <summary>
    /// Retrieves and displays the total count of customers from the database.
    /// </summary>
    /// <remarks>
    /// This method queries the database using Entity Framework to count the total number of customers
    /// and displays the result in a formatted panel.
    /// </remarks>
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

    /// <summary>
    /// Updates a customer's details in the database by their unique identifier.
    /// </summary>
    /// <remarks>
    /// This method retrieves a customer with a specific ID, modifies their company name, 
    /// and saves the changes back to the database. If the customer is not found, 
    /// an error message is displayed. If the update is successful, a success message is shown.
    /// </remarks>
    /// <exception cref="DbUpdateException">
    /// Thrown if an error occurs while saving changes to the database.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the customer entity cannot be attached to the context.
    /// </exception>
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
}
