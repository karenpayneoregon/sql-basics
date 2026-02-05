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

        DataOperations.DisplayTop5Customers();
        DataOperations.UpdateCustomerById();
        DataOperations.DisplayTop5Customers();
        DataOperations.GetCustomersCount();
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
