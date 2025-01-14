using DapperGetDatabaseAndTableNamesApp.Classes;
using SqlServer.Library.Classes;
using SqlServer.Library.Models;

namespace DapperGetDatabaseAndTableNamesApp;

internal partial class Program
{
    private static void Main()
    {
        var result = GeneralUtilities.ExpressDatabaseExists("NorthWind2024");
        while (true)
        {
            Console.Clear();

            var menuItem = AnsiConsole.Prompt(MenuOperations.MainSelectionPrompt());
            if (menuItem.Id != -1)
            {
                menuItem.Action();
            }
            else
            {
                return;
            }
        }
        
    }


}