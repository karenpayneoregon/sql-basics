using EnumHasConversionDapper.Classes;
using EnumHasConversionDapper.Models;


namespace EnumHasConversionDapper;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        AddWineIfNotExists();
        GetAllWines();
        WhiteWines();
        GetAllWinesByType();
        GroupedWines();

        await WhereInMultipleWineTypes();

        Console.ReadLine();
    }

    private static void WhiteWines()
    {
        WineOperations operations = new();
        var wines = operations.AllWines();
        List<Wine> list = [.. wines.Where(wine => wine.WineType == WineType.White)];

        Present(list, "White wines");
    }

    /// <summary>
    /// Read all wines
    /// </summary>
    private static void GetAllWines()
    {
        WineOperations operations = new();
        var wines = operations.AllWines();
        Present(wines, "All wines ORDER BY Name, WineType");
    }

    /// <summary>
    /// Read wines by <see cref="WineType"/>
    /// </summary>
    private static void GetAllWinesByType()
    {
        WineOperations operations = new();
        var wines = operations.SpecificWines(WineType.Red);
        Present(wines, $"{WineType.Red} wines");
    }

    /// <summary>
    /// Read all wines grouped by <see cref="WineType"/>
    /// </summary>
    private static void GroupedWines()
    {
        AnsiConsole.MarkupLine($"[cyan]Grouped[/]");
        WineOperations operations = new();
        var wines = operations.AllWines();

        List<WineGroupItem> allWinesGrouped1 = wines
            .GroupBy(wine => wine.WineType)
            .Select(w => new WineGroupItem(w.Key, w.ToList()))
            .ToList();

        foreach (WineGroupItem item in allWinesGrouped1)
        {
            AnsiConsole.MarkupLine($"    [white]{item.Type}[/]");
            foreach (var wine in item.List)
            {
                AnsiConsole.MarkupLine($"        [white]{wine.WineId,-27}[/][yellow]{wine.Name}[/]");
            }
        }
    }

    private static async Task WhereInMultipleWineTypes()
    {
        WineOperations operations = new();
        string[] wineTypes = ["Red", "White"];
        var list = await operations.WhereIn(wineTypes);
        Present(list, "White and red wines");
    }

    /// <summary>
    /// Add a wine if not currently in the Wine table, get the primary key
    /// </summary>
    private static void AddWineIfNotExists()
    {
        var wineName = "Chardonnay";
        var wineType = WineType.White;
        WineOperations operations = new();
        
        if (operations.WineExist(wineName, wineType))
        {
            AnsiConsole.MarkupLine($"[red]Wine {wineName} already exists[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[green]Adding {wineName}[/]");
            int identifier = operations.AddWine(wineName, wineType);
        }
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

