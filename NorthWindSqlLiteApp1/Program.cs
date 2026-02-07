using NorthWindSqlLiteApp1.Classes;
using NorthWindSqlLiteApp1.Classes.Core;
using Spectre.Console;

namespace NorthWindSqlLiteApp1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Task.Delay(0);
        
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

        UtilityCode.GetModelNames();

        CustomerOperations.CustomersFormattableString();
        
        await CustomerOperations.SortCustomerOnContactTitle();
        
        Console.WriteLine();

        EmployeeOperations.ReportsToManager();

        Console.WriteLine();

        EmployeeOperations.GetSingleEmployee();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
