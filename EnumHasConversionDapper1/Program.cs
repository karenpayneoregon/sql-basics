using EnumHasConversionDapper1.Classes;
using EnumHasConversionDapper1.Models;


namespace EnumHasConversionDapper1;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        WineOperations operations = new();

        GetAllWines(operations);
        GetRedWines(operations);
        ZinfandelWineExists(operations);
        await WhereInWines(operations);
        
        Console.ReadLine();
    }

    private static async Task WhereInWines(WineOperations operations)
    {
        WineType[] enums = [WineType.Rose, WineType.White];
        var list = await operations.WhereIn(enums);

        Present(list, "WhereIn Rose and White wines");
    }

    private static void ZinfandelWineExists(WineOperations operations)
    {
        var zinfandelExists = operations.WineExist("Zinfandel", WineType.Rose);
        AnsiConsole.MarkupLine($"[cyan]Zinfandel exists:[/] [yellow]{zinfandelExists}[/]");
    }

    private static void GetRedWines(WineOperations operations)
    {
        var wines = operations.SpecificWines(WineType.Red);
        Present(wines, "Red wines");
    }

    private static void GetAllWines(WineOperations operations)
    {
        var wines = operations.AllWines();
        Present(wines, "All wines");
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