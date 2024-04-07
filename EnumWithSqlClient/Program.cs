using EnumWithSqlClient.Classes;
using EnumWithSqlClient.Models;

namespace EnumWithSqlClient;

internal partial class Program
{
    static void Main(string[] args)
    {
        GetAllWines();
        GetSpecificWineType();
        WhereInSample();
        Console.ReadLine();
    }

    private static void WhereInSample()
    {
        var wineTypes = new WineType[] { WineType.White, WineType.Red };
        var wines = WineOperations.WhereIn(wineTypes);
        Present(wines, "Multiples");
    }

    private static void GetSpecificWineType()
    {
        var wines = WineOperations.WinesByType(WineType.White);
        Present(wines, "White");
    }

    private static void GetAllWines()
    {
        var wines = WineOperations.AllWines();
        Present(wines, "All");
    }

    /// <summary>
    /// Display all wines in list
    /// </summary>
    /// <param name="list">List of <see cref="Wine"/></param>
    /// <param name="description">Text to display</param>
    private static void Present(List<Wine> list, string description)
    {
        AnsiConsole.MarkupLine($"[cyan]{description}[/]");
        foreach (var wine in list)
        {
            AnsiConsole.MarkupLine($"    [white]{wine.Name,-30}[/] [yellow]{wine.WineType}[/]");
        }

        Console.WriteLine();
    }
}