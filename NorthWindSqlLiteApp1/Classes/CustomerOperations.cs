using Diacritics.Extensions;
using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Classes.Core;
using NorthWindSqlLiteApp1.Classes.Extensions;
using NorthWindSqlLiteApp1.Classes.MemberAccess;
using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Mappings;
using NorthWindSqlLiteApp1.Models;
using NorthWindSqlLiteApp1.Models.Projections;
using NorthWindSqlLiteApp1.Models.Sorting;
using Spectre.Console;
using System.Diagnostics;
using System.Text;
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
///
/// IgnoreQueryFilters see Cotext.cs HasQueryFilter.
/// </remarks>
internal class CustomerOperations
{
    /// <summary>
    /// Displays the top 5 customers from the database.
    /// </summary>
    public static void DisplayTop5Customers()
    {

        PrintPink();

        using var context = new Context();
        var customers = context.Customers
            .IgnoreQueryFilters()
            .TagWithDebugInfo("With ignore filter")
            .AsNoTracking()
            .Take(5)
            .ToList();
        customers.ForEach(c =>
            Console.WriteLine($"CustomerID: {c.CustomerIdentifier}, CompanyName: {c.CompanyName}"));

        Console.WriteLine();

    }

    /// <summary>
    /// Retrieves a customer with related data using multiple joins.
    /// </summary>
    /// <remarks>
    /// This method fetches a customer with a specific identifier and includes related entities such as:
    /// <list type="bullet">
    /// <item>Contact information</item>
    /// <item>Contact type</item>
    /// <item>Country details</item>
    /// <item>Orders and their details</item>
    /// <item>Products and their suppliers</item>
    /// <item>Supplier's country details</item>
    /// </list>
    /// </remarks>
    public static void CustomerWithJoins()
    {

        PrintPink();

        using (var context = new Context())
        {
            var customer = context
                .Customers
                .IgnoreQueryFilters()
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
    /// Retrieves a single customer and their associated details using a projection.
    /// </summary>
    /// <remarks>
    /// This method queries the database to fetch a customer with a specific identifier. 
    /// It includes related data such as contact information, contact type, and country details. 
    /// The result is projected into a <see cref="CustomerWithContact"/> object for simplified access to the data.
    /// </remarks>
    public static void WithProjection(int id = 1)
    {
        PrintPink();

        using (var context = new Context())
        {
            var customer = context
                .Customers
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Include(x => x.Contact)
                .Include(x => x.ContactTypeIdentifierNavigation)
                .Include(x => x.CountryIdentifierNavigation)
                .Select(x => new CustomerWithContact(
                    x.CustomerIdentifier, 
                    x.CompanyName, 
                    x.Contact.FirstName, 
                    x.Contact.LastName, 
                    x.ContactTypeIdentifierNavigation.ContactTitle, 
                    x.CountryIdentifierNavigation.Name))
                .FirstOrDefault(x => x.CustomerIdentifier == id);
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
            var count = context.Customers
                .AsNoTracking()
                .Count();
            PanelDisplay("Total Customers", $"Count: {count}");
        }

        Console.WriteLine();

    }

    /// <summary>
    /// Asynchronously retrieves the total count of customers from the NorthWind SQLite database using Dapper.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="MemberAccessSamples.GetCustomerCountUsingDapper"/> method to fetch the customer count.
    /// It also displays the count in the console using a formatted output.
    /// </remarks>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    /// <example>
    /// Example usage:
    /// <code>
    /// await CustomerOperations.GetCustomersCountWithDapper();
    /// </code>
    /// </example>
    public static async Task GetCustomersCountWithDapper()
    {
        PrintPink();
        
        var customerCount = await MemberAccessSamples.GetCustomerCountUsingDapper();
        if (customerCount > -1)
        {
            AnsiConsole.MarkupLine($"[green bold]Total Customers (Dapper): {customerCount}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to retrieve customer count using Dapper.[/]");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Retrieves the total count of customers from the database, ignoring any query filters.
    /// </summary>
    /// <remarks>
    /// This method uses Entity Framework Core's <see cref="EntityFrameworkQueryableExtensions.IgnoreQueryFilters"/> 
    /// to bypass any global query filters applied to the <see cref="Context.Customers"/> DbSet.
    /// The result is displayed in a formatted panel using Spectre.Console.
    /// </remarks>
    /// <example>
    /// <code>
    /// DataOperations.GetCustomersCountIgnoreQueryFilters();
    /// </code>
    /// </example>
    public static void GetCustomersCountIgnoreQueryFilters()
    {

        PrintPink();

        using (var context = new Context())
        {
            var count = context.Customers
                .IgnoreQueryFilters() // NOTE: This will ignore any global query filters defined in the DbContext
                .AsNoTracking()
                .Count();
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
            var customer = context.Customers
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefault(x => x.CustomerIdentifier == 3);

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
                ErrorPill(Justify.Left, "Cannot delete customer with ID 15 because there are related orders.");
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

    /// <summary>
    /// Adds a new customer to the NorthWind SQLite database.
    /// </summary>
    /// <remarks>
    /// This method creates a new customer with predefined details and saves it to the database.
    /// It uses the <see cref="Context"/> class for database operations and displays a success or error message
    /// using Spectre.Console helpers.
    /// </remarks>
    /// <example>
    /// <code>
    /// CustomerOperations.AddCustomer();
    /// </code>
    /// </example>
    /// <exception cref="DbUpdateException">
    /// Thrown when an error occurs while saving changes to the database.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the database context is not properly configured.
    /// </exception>
    public static void AddCustomer()
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


    /// <summary>
    /// Adds a new customer to the database and displays the Entity Framework Core change tracker debug view.
    /// </summary>
    /// <remarks>
    /// This method creates a new customer with predefined details and adds it to the database context.
    /// It then outputs the detailed debug view of the change tracker to the console, which provides
    /// insights into the state of tracked entities within the context.
    /// </remarks>
    public static void AddCustomerDebugView()
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

            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

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

            var customers = context.Customers
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(c => customerIds.Contains(c.CustomerIdentifier))
                .ToList();

            if (customers.Any())
            {
                customers.ForEach(c => Console.WriteLine($"{c.CustomerIdentifier,-4}{c.CompanyName}"));
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
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(c => companyNames.Contains(c.CompanyName))
                .ToList();

            if (customers.Any())
            {
                customers.ForEach(c => Console.WriteLine($"{c.CustomerIdentifier,-4}{c.CompanyName}"));
            }
            else
            {
                InfoPill(Justify.Left, "No customers found for the specified company names.");
            }
        }

        Console.WriteLine();
    }


    /// <summary>
    /// Asynchronously sorts customers based on their contact title in descending order,
    /// displays the sorted data in a formatted table, and outputs it to the console.
    /// </summary>
    /// <remarks>
    /// This method retrieves customer data from the database, including related contact
    /// and contact type information, while ignoring query filters. The data is then
    /// sorted by the contact title in descending order using the <see cref="PropertyName.Title"/> 
    /// property and displayed in a table format using Spectre.Console.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="DbUpdateException">Thrown if there is an issue updating the database context.</exception>
    /// <example>
    /// <code>
    /// await CustomerOperations.SortCustomerOnContactTitle();
    /// </code>
    /// </example>
    public static async Task SortCustomerOnContactTitle()
    {
        await using var context = new Context();

        List<Customers> customers = await context.Customers
            .IgnoreQueryFilters()
            .Include(c => c.Contact)
            .Include(c => c.ContactTypeIdentifierNavigation)
            .OrderByEnum(PropertyName.Title, Direction.Descending)
            .ToListAsync();

        var table = CreateTableForContactTitle();

        for (int index = 0; index < customers.Count; index++)
        {
            table.AddRow(
                customers[index].CompanyName,
                customers[index].ContactTypeIdentifierNavigation.ContactTitle,
                customers[index].Contact.LastName);
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
        Console.WriteLine();

    }

    /// <summary>
    /// Retrieves and displays a list of customers filtered by a specific contact type identifier.
    /// </summary>
    /// <remarks>
    /// This method constructs a SQL query using a <see cref="FormattableString"/> to filter customers
    /// by a predefined contact type identifier. It executes the query using Entity Framework Core's
    /// <see cref="Microsoft.EntityFrameworkCore.RelationalQueryableExtensions.FromSqlInterpolated{TEntity}(IQueryable{TEntity}, FormattableString)"/> 
    /// method, applies query filters, and includes related contact data.
    /// 
    /// The results are displayed in a formatted output, showing the customer identifier, company name,
    /// and the full name of the associated contact.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// DataOperations.CustomersFormattableString();
    /// </code>
    /// </example>
    /// <seealso cref="Microsoft.EntityFrameworkCore.RelationalQueryableExtensions.FromSqlInterpolated{TEntity}(IQueryable{TEntity}, FormattableString)"/>
    /// <seealso cref="Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IgnoreQueryFilters{TEntity}(IQueryable{TEntity})"/>
    public static void CustomersFormattableString()
    {

        PrintPink();

        const int contactTypeIdentifier = 5;

        FormattableString statement =
            $"""
             SELECT *
              FROM Customers
             WHERE ContactTypeIdentifier = {contactTypeIdentifier}
             """;

        using var context = new Context();
        var list = context.Customers
            .FromSqlInterpolated(statement)
            .IgnoreQueryFilters()
            .Include(c => c.Contact)
            .ToList();

        foreach (var customer in list)
        {
            Console.WriteLine($"{customer.CustomerIdentifier,-4}" +
                              $"{customer.CompanyName,-35}" +
                              $"{customer.Contact.FullName}");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Updates the city name for customers in the database from <b>"MÃ©xico D.F."</b> to <b>"Mexico D.F."</b>.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// <list type="bullet">
    /// <item>Counts the number of customers with the city name "MÃ©xico D.F.".</item>
    /// <item>Updates all occurrences of "MÃ©xico D.F." in <br/>the <see cref="Customers.City"/> property to "Mexico D.F.".</item>
    /// <item>Counts the number of customers with the city name "MÃ©xico D.F." after the update to verify the changes.</item>
    /// </list>
    /// 
    /// The method uses Entity Framework Core's <a href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalqueryableextensions.executeupdateasync?view=efcore-8.0">ExecuteUpdateAsync</a> for efficient bulk updates and 
    /// <a href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.ignorequeryfilters?view=efcore-10.0">IgnoreQueryFilters</a> to bypass any global query filters applied to the <see cref="Customers"/> entity.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task FixCityMexico()
    {

        var name = "MÃ©xico D.F.";
        await using var context = new Context();
        
        Console.WriteLine(context.Customers.IgnoreQueryFilters().Count(x => x.City == name));
        
        await context.Customers
            .IgnoreQueryFilters()
            .Where(e => e.City == name)
            .ExecuteUpdateAsync(cust => 
                cust.SetProperty(b => b.City, "Mexico D.F."));

        // Save changes is not needed when using ExecuteUpdateAsync as
        // it executes the update directly in the database without tracking changes in the context.

        Console.WriteLine(context.Customers.IgnoreQueryFilters().Count(x => x.City == name));
    }

    public static async Task CityRemoveDiacritics()
    {
        await using var context = new Context();

        var customers = context.Customers
            .IgnoreQueryFilters()
            .OrderBy(c => c)
            .ToList();


        foreach (var customer in customers.Where(customer => customer.City.HasDiacritics()))
        {
            context.Entry(customer).Property(c => c.City).CurrentValue = 
                customer.City.RemoveDiacritics(new CityNameAccentsMapping());
        }

        Console.WriteLine(await context.SaveChangesAsync());
    }

    private static Table CreateTableForContactTitle()
    {
        return new Table()
            .RoundedBorder()
            .BorderColor(Color.LightSlateGrey)
            .AddColumn("[b]Customer[/]")
            .AddColumn("[b]Title[/]")
            .AddColumn("[b]Contact last name[/]")
            .Title("[cyan]By title[/]")
            .Alignment(Justify.Center);
    }
}