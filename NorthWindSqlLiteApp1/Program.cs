using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Classes;
using NorthWindSqlLiteApp1.Classes.Core;
using NorthWindSqlLiteApp1.Data;
using Spectre.Console;

namespace NorthWindSqlLiteApp1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Task.Delay(0);
        
        DataOperations.GetCustomersCount();
        DataOperations.GetCustomersCountIgnoreQueryFilters();
        
        DataOperations.DisplayTop5Customers();
        DataOperations.CustomerWithJoins();
        
        DataOperations.UpdateCustomerById();
        DataOperations.DisplayTop5Customers(); // Display updated customer
        
        DataOperations.RemoveCustomerDenied();
        
        DataOperations.GetCustomersCount();
        
        DataOperations.WhereInCustomersIdentifiers();
        DataOperations.WhereInCompanyName();
        
        DataOperations.AddCustomer();
        DataOperations.AddCustomerDebugView();
        
        DataOperations.GetCustomersCountIgnoreQueryFilters();
        
        DataOperations.GetModelNames();

        await DataOperations.SortCustomerOnContactTitle();
        
        DataOperations.CustomersFormattableString();
    
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
