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
            customers.ForEach(c => 
                Console.WriteLine($"CustomerID: {c.CustomerIdentifier}, CompanyName: {c.CompanyName}"));
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

    /// <summary>
    /// Attempts to remove a customer with a specific identifier from the database.
    /// </summary>
    /// <remarks>
    /// This method checks if the customer with the specified identifier has any related orders.
    /// If related orders exist, the deletion is denied, and an error message is displayed.
    /// If no related orders are found, the customer is marked for deletion, and the changes are saved to the database.
    /// Feedback is provided on whether the operation was successful or not.
    ///
    /// Perfect candidate for soft delete implementation.
    /// 
    /// </remarks>
    public static void RemoveCustomerDenied()
    {
        PrintPink();

        using (var context = new Context())
        {
            // check for related orders before attempting to delete the customer
            if (context.Orders.FirstOrDefault(x => x.CustomerIdentifier == 15) != null)
            {
                ErrorPill(Justify.Left,"Cannot delete customer with ID 15 because there are related orders.");
                return;
            }
            
            var customerToDelete = context.Customers
                .AsNoTracking()
                .FirstOrDefault(c => c.CustomerIdentifier == 15);

            if (customerToDelete != null)
            {
                context.Customers.Attach(customerToDelete).State = EntityState.Deleted;
                var result = context.SaveChanges();

                if (result == 1)
                {
                    SuccessPill(Justify.Left, $"Customer with ID {customerToDelete.CustomerIdentifier} removed successfully.");
                }
                else
                {
                    ErrorPill(Justify.Left, $"Customer with ID {customerToDelete.CustomerIdentifier} was not removed.");
                }
            }
            else
            {
                ErrorPill(Justify.Left, "Customer not found with the specified ID.");
            }
        }

        Console.WriteLine();
    }
    
    public static void AddCustomer()
    {
        
        PrintPink();
        
        using (var context = new Context())
        {
            var newCustomer = new Models.Customers
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

    /// <summary>
    /// Retrieves and displays customers whose identifiers match a specified list of IDs.
    /// </summary>
    /// <remarks>
    /// This method queries the database for customers whose <c>CustomerIdentifier</c> matches
    /// any of the IDs in a predefined list. The results are displayed in the console.
    /// If no customers are found, a message indicating this is displayed.
    /// </remarks>
    public static void WhereInCustomersIdentifiers()
    {
        PrintPink();

        using (var context = new Context())
        {
            var customerIds = new List<int> { 1, 5, 30, 78 }; 

            var customers = context.Customers.AsNoTracking()
                .Where(c => customerIds.Contains(c.CustomerIdentifier))
                .ToList();

            if (customers.Any())
            {
                customers.ForEach(c => Console.WriteLine($"{c.CustomerIdentifier, -4}{c.CompanyName}"));
            }
            else
            {
                InfoPill(Justify.Left, "No customers found for the specified IDs.");
            }
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Retrieves a list of customers whose company names match the specified list of company names.
    /// </summary>
    /// <remarks>
    /// This method queries the database using Entity Framework Core to find customers with company names
    /// that are included in a predefined list. The results are displayed in the console.
    /// </remarks>
    public static void WhereInCompanyName()
    {
        PrintPink();

        using (var context = new Context())
        {
            List<string> companyNames = 
                [
                    "Alfreds Futterkiste", 
                    "Around the Horn"
                ]; 

            var customers = context.Customers
                .AsNoTracking()
                .Where(c => companyNames.Contains(c.CompanyName))
                .ToList();

            if (customers.Any())
            {
                customers.ForEach(c => Console.WriteLine($"{c.CustomerIdentifier, -4}{c.CompanyName}"));
            }
            else
            {
                InfoPill(Justify.Left, "No customers found for the specified company names.");
            }
        }

        Console.WriteLine();
    }
}
