using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Classes;
using NorthWindSqlLiteApp1.Classes.Core;
using NorthWindSqlLiteApp1.Data;
using Spectre.Console;

namespace NorthWindSqlLiteApp1;
internal partial class Program
{
    static void Main(string[] args)
    {

        DataOperations.GetCustomersCount();
        DataOperations.DisplayTop5Customers();
        DataOperations.CustomerWithJoins();
        DataOperations.UpdateCustomerById();
        DataOperations.DisplayTop5Customers(); // Display updated customer
        DataOperations.RemoveCustomerDenied();
        DataOperations.GetCustomersCount();
        DataOperations.WhereInCustomersIdentifiers();
        DataOperations.WhereInCompanyName();
        DataOperations.AddCustomer();
        DataOperations.GetCustomersCount();


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
