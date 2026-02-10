using NorthWindSqlLiteApp1.Classes;
using NorthWindSqlLiteApp1.Classes.Core;
using NorthWindSqlLiteApp1.Classes.MemberAccess;
using Spectre.Console;

namespace NorthWindSqlLiteApp1;
internal partial class Program
{
    private static async Task Main(string[] args)
    {
        await Task.Delay(0);

        //MemberAccessSamples.NullCondition();

        await PerformCustomerOperations();
        //PerformEmployeeOperations();
        //await PerformOrderOperations();

        //Console.WriteLine();
        //UtilityCode.GetModelNames();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Executes a series of operations related to orders, including retrieving a single order,
    /// modifying order properties, and creating a new order.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations sequentially:
    /// - Retrieves and displays details of a single order using <see cref="OrderOperations.GetSingleOrderByIdentifier(int)"/>.
    /// - Alters property values of an order without saving changes using <see cref="OrderOperations.AlterPropertyValue(int)"/>.
    /// - Creates a new order using <see cref="OrderOperations.CreateNewOrder()"/>.
    /// 
    /// These operations utilize the <c>OrderOperations</c> class to interact with the database.
    /// </remarks>
    private static async Task PerformOrderOperations()
    {
        await OrderOperations.GetSingleOrderByIdentifier();
        OrderOperations.AlterPropertyValue();
        OrderOperations.CreateNewOrder();
    }

    /// <summary>
    /// Executes operations related to <seealso cref="Models.Employees"/>, including displaying hierarchical
    /// relationships between employees and their managers, and retrieving details
    /// of a single employee.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="EmployeeOperations.ReportsToManager"/> 
    /// method to display a tree structure of employees and their managers, and the 
    /// <see cref="EmployeeOperations.GetSingleEmployee"/> 
    /// method to fetch and display details of a specific employee.
    /// </remarks>
    private static void PerformEmployeeOperations()
    {
        EmployeeOperations.ReportsToManager();
        EmployeeOperations.GetSingleEmployee();
    }

    /// <summary>
    /// Executes a series of <b>customer</b>-related operations on the <b>NorthWind SQLite database</b>.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// <list type="number">
    /// <item>Retrieves and displays the total count of customers.</item>
    /// <item>Displays the top 5 customers.</item>
    /// <item>Retrieves customer details with related data.</item>
    /// <item>Updates customer information and displays the updated data.</item>
    /// <item>Removes a customer with specific restrictions.</item>
    /// <item>Executes queries using `WHERE IN` clauses for customer identifiers and company names.</item>
    /// <item>Adds new customers and displays debug information.</item>
    /// <item>Sorts customers based on their contact titles asynchronously.</item>
    /// 
    /// The method utilizes the <see cref="CustomerOperations"/> class for database interactions
    /// and leverages Entity Framework Core for querying and updating data.
    /// </list>
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private static async Task PerformCustomerOperations()
    {
        CustomerOperations.GetCustomersCount();
        CustomerOperations.GetCustomersCountIgnoreQueryFilters();

        CustomerOperations.DisplayTop5Customers();
        CustomerOperations.CustomerWithJoins();

        CustomerOperations.UpdateCustomerById();
        CustomerOperations.DisplayTop5Customers(); // Display updated customer

        CustomerOperations.RemoveCustomerDenied();

        CustomerOperations.GetCustomersCount();

        CustomerOperations.WhereInCustomersIdentifiers();
        CustomerOperations.WhereInCompanyName();

        CustomerOperations.AddCustomer();
        CustomerOperations.AddCustomerDebugView();

        CustomerOperations.GetCustomersCountIgnoreQueryFilters();
        
        CustomerOperations.CustomersFormattableString();

        await CustomerOperations.SortCustomerOnContactTitle();
    }
}
