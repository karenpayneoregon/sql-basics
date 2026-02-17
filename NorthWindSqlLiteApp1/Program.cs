using NorthWindSqlLiteApp1.Classes;
using NorthWindSqlLiteApp1.Classes.Core;
using NorthWindSqlLiteApp1.Classes.MemberAccess;
using NorthWindSqlLiteApp1.Models;
using Spectre.Console;

namespace NorthWindSqlLiteApp1;
internal partial class Program
{
    private static async Task Main(string[] args)
    {
        await Task.Delay(0);
        //await Warmup();
        //MemberAccessSamples.NullCondition();
        //await ExecuteImplicitOperators();

        await PerformCustomerOperations();
        //await CustomerOperations.FixCityMexico();
        //PerformEmployeeOperations();
        //await PerformOrderOperations();

        //Console.WriteLine();
        //UtilityCode.GetModelNames();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Executes implicit operator demonstrations for products and categories.
    /// </summary>
    /// <remarks>
    /// This method calls the implicit operator demonstration methods from 
    /// <see cref="ProductsOperations"/> and <see cref="CategoryOperations"/>.
    /// These methods showcase the conversion of database entities into their 
    /// corresponding DTOs using implicit operators.
    /// </remarks>
    /// <seealso cref="ProductsOperations.ImplicitOperator"/>
    /// <seealso cref="CategoryOperations.ImplicitOperator"/>
    private static async Task ExecuteImplicitOperators()
    {
        await ProductsOperations.ImplicitOperator();
        await CategoryOperations.ImplicitOperator();
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
        
        await CustomerOperations.GetCustomersCountWithDapper();
        
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
