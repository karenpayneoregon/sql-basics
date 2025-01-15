using DapperGetDatabaseAndTableNamesApp.Classes;
using SqlServer.Library.Classes;


namespace DapperGetDatabaseAndTableNamesApp;

internal partial class Program
{
    private static void Main()
    {
        var result = GeneralUtilities.ExpressDatabaseExists("NorthWind2024");
        var result1 = GeneralUtilities.ExpressDatabaseExists1("NorthWind2024");
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