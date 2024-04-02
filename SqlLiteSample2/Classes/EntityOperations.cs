
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.EntityFrameworkCore;
using SqlLiteSample2.Data;
using SqlLiteSample2.Models;
using SqlLiteSample2.Validators;
using static SqlLiteSample2.Classes.SpectreConsoleHelpers;
//using ValidationResult = Spectre.Console.ValidationResult;

/*
 * NOTES:
 * FL is an alias at the end of the project file
 */


namespace SqlLiteSample2.Classes;
internal class EntityOperations
{

    /// <summary>
    /// Get all table names in NorthWind2024 database
    /// </summary>
    public static async Task GetTableNamesInDatabase()
    {

        PrintMethod();

        List<string> tableNames = await DapperOperations.TableNames();
        await File.WriteAllTextAsync("Results\\TableNames.txt", ObjectDumper.Dump(tableNames));

    }

    /// <summary>
    /// Update customer city from MÃ©xico D.F. to Mexico City using ExecuteUpdateAsync
    /// </summary>
    /// <returns></returns>
    public static async Task UpdateCustomerCity()
    {

        PrintMethod();

        await using var context = new Context();

        string city = "MÃ\u00a9xico D.F.";
        string value = "Mexico City";

        await context.Customers
            .Where(ud => ud.City == city)
            .ExecuteUpdateAsync(x => x
                .SetProperty(u => u.City, value));

    }
    /// <summary>
    /// Remove an existing order wrapped in a transaction
    /// </summary>
    public static async Task RemoveOneOrder()
    {

        PrintMethod();

        await using var context = new Context();

        /*
         * Get specific order details by order identifier
         * We use a variable to properly parameterize the query
         */
        var orderIdentifier = 10248;
        var orderDetails = await context.OrderDetails
            .AsNoTracking()
            .Include(x => x.Order)
            .Include(x => x.Product)
            .ThenInclude(x => x.Category)
            .Where(x => x.OrderID == orderIdentifier)
            .ToListAsync();

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            foreach (var detail in orderDetails)
            {
                context.Entry(detail).State = EntityState.Deleted;
            }

            var order = await context.Orders.FindAsync(orderIdentifier);

            context.Entry(order!).State = EntityState.Deleted;

            var result = await context.SaveChangesAsync();
            await transaction.CommitAsync();

        }
        catch (Exception ex)
        {
            await File.WriteAllTextAsync("Results\\RemoveOrderError.txt", ex.Message);
        }
    }

    /// <summary>
    /// Read all employees and update the first one
    /// </summary>
    public static async Task ReadAllEmployeesUpDateOne()
    {

        PrintMethod();

        await using var context = new Context();

        var employees = await context.Employees
            .AsNoTracking()
            .Include(x => x.CountryIdentifierNavigation)
            .Include(x => x.ContactTypeIdentifierNavigation)
            .Include(x => x.Territory)
            .ToListAsync();


        // update first employee
        var firstEmployee = employees.FirstOrDefault();
        firstEmployee!.FirstName = "Karen";
        firstEmployee.LastName = "Payne";

        EmployeesValidator validator = new();
        FL.Results.ValidationResult validate = await validator.ValidateAsync(firstEmployee);
        if (validate.IsValid)
        {
            context.Entry(firstEmployee).State = EntityState.Modified;

            var result = await context.SaveChangesAsync();

            AnsiConsole.MarkupLine(result == 1 ? "   [hotpink]Employee saved[/]" : "   [red]Employee not saved[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("   [red]Employee not valid[/]");
        }

    }

    /// <summary>
    /// Read all categories, write results to file
    /// </summary>
    public static async Task ReadAllCategories()
    {

        PrintMethod();

        await using var context = new Context();

        var categories = await context.Categories
            .AsNoTracking()
            .Include(x => x.Products)
            .ThenInclude(x => x.Supplier)
            .ToListAsync();


        await File.WriteAllTextAsync("Results\\Categories.txt", ObjectDumper.Dump(categories));
    }

    /// <summary>
    /// Add a new contact
    /// </summary>
    public static async Task AddNewContact()
    {

        PrintMethod();

        await using var context = new Context();

        Contacts contacts = new()
        {
            FirstName = "Karen",
            LastName = "Payne",
            FullName = "Karen Payne",
            ContactTypeIdentifier = 7 // Owner
        };
        
        ContactValidator validator = new();
        var validate = await validator.ValidateAsync(contacts);
        if (validate.IsValid)
        {
            context.Contacts.Add(contacts);

            var result = await context.SaveChangesAsync();

            AnsiConsole.MarkupLine(result == 1 ? "   [hotpink]Contacted added[/]" : "   [red]Contacted not added[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("   [red]Contact not valid[/]");
        }

    }

    /// <summary>
    /// In this example, two rules are violated.
    /// 1. Missing last name
    /// 2. ContactType is invalid
    /// </summary>
    /// <returns></returns>
    public static async Task AddNewContactBad()
    {

        PrintMethod();

        await using var context = new Context();

        Contacts contacts = new()
        {
            FirstName = "Karen",
            FullName = "Karen Payne",
            ContactTypeIdentifier = 27 // Does not exist
        };

        ContactValidator validator = new();
        var validate = await validator.ValidateAsync(contacts);
        if (validate.IsValid)
        {
            context.Contacts.Add(contacts);

            var result = await context.SaveChangesAsync();

            AnsiConsole.MarkupLine(result == 1 ? "   [hotpink]Contacted added[/]" : "   [red]Contacted not added[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("   [red]Contact not valid[/]");
            foreach (var failure in validate.Errors)
            {
                AnsiConsole.MarkupLine($"        [white]{failure.ErrorMessage}[/]");
            }
        }

    }
    /// <summary>
    /// Read all customers and write to a file
    /// </summary>
    public static async Task ReadAllCustomers()
    {

        PrintMethod();
            
        await using var context = new Context();

        var customers = await context.Customers
            .AsNoTracking()
            .Include(c => c.CountryIdentifierNavigation)
            .Include(c => c.ContactTypeIdentifierNavigation)
            .Include(c => c.Contact)
            .ToListAsync();

        await File.WriteAllTextAsync("Results\\Customers.txt", ObjectDumper.Dump(customers));

    }

    /// <summary>
    /// This method shows how to validate two properties while ignoring the rest
    /// * CompanyName
    /// * ContactId
    /// * CountryIdentifier
    /// </summary>
    public static void AddNewCustomerValidateOneProperty()
    {
        
        PrintMethod();

        Customers customers = new()
        {
            //City = "Mexico City",
            Street = "123 Main Street"
        };

        //var properties = new[] { nameof(Customers.Street), nameof(Customers.City) };
        //ValidationContext<Customers> context = new(customers, 
        //    new PropertyChain(), 
        //    new MemberNameValidatorSelector(properties));

        //IValidator validate = new CustomersValidator();
        //var result = validate.Validate(context);
        //Console.WriteLine(result.IsValid ? "Valid" : "Not valid");

        var valid = CustomersCityStreetValidator.Validate(customers);
        Console.WriteLine(valid ? "Valid" : "Not valid");
    }
}
