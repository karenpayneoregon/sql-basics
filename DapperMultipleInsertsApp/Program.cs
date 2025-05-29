using ConsoleConfigurationLibrary.Classes;
using Dapper;
using DapperMultipleInsertsApp.Classes;
using Microsoft.Data.SqlClient;

namespace DapperMultipleInsertsApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        var count = DapperOperations.InsertCategories();

        AnsiConsole.MarkupLine($"[white]{count}[/] rows inserted.");
        Console.WriteLine();
        var list = DapperOperations.ViewCategories();
        foreach (var category in list)
        {
            AnsiConsole.MarkupLine($"[cyan]{category.Id,-5}[/] {category.CategoryName,-25}{category.Description}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }


}