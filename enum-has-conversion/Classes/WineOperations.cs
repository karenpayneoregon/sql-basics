using EnumHasConversion.Data;
using EnumHasConversion.Models;
using System.Text.Json;
using static EnumHasConversion.Classes.SpectreConsoleHelpers;

namespace EnumHasConversion.Classes;

public class WineOperations
{
    /// <summary>
    /// Retrieves a list of wines based on the specified wine type.
    /// </summary>
    /// <param name="wineType">The wine type to filter by.</param>
    /// <returns>A list of wines matching the specified wine type.</returns>
    public static List<Wine> GetWinesByType(WineType wineType)
    {
        using var context = new WineContext();
        return [.. context.Wines.Where(wine => wine.WineType == wineType)];
    }

    public static void RunExamples()
    {

        using var context = new WineContext();

        var allWine = context.Wines.ToList();

        LineSeparator("[white]Grouped 1[/]");

        List<WineGroupItem> allWinesGrouped1 = context.Wines
            .GroupBy(wine => wine.WineType)
            .Select(w => new WineGroupItem(w.Key, w.ToList()))
            .ToList();

        Dictionary<WineType, List<Wine>> allWinesGrouped2 = context.Wines.GroupBy(x => x.WineType)
            .ToDictionary(k => k.Key, v => v.ToList());

        foreach (WineGroupItem item in allWinesGrouped1)
        {
            AnsiConsole.MarkupLine($"[cyan]{item.Type}[/]");
            foreach (var wine in item.List)
            {
                Console.WriteLine($"\t{wine.WineId,-5}{wine.Name}");
            }
        }

        LineSeparator("[white]Grouped 2[/]");

        foreach (var (wineType, wineList) in allWinesGrouped2)
        {
            AnsiConsole.MarkupLine($"[cyan]{wineType}[/]");
            foreach (var wine in wineList)
            {
                Console.WriteLine($"\t{wine.WineId,-5}{wine.Name}");
            }
        }

        List<Wine> allWines = [.. context.Wines];

        LineSeparator("[white]All[/]");

        foreach (var wine in allWines)
        {
            AnsiConsole.MarkupLine($"[cyan]{wine.WineType,-8}[/]{wine.Name}");
        }

        List<Wine> rose = [.. context.Wines.Where(wine => wine.WineType == WineType.Rose)];

        LineSeparator("[white]Rose[/]");

        if (rose.Count == 0)
        {
            Console.WriteLine("\tNone");
        }
        else
        {
            foreach (var wine in rose)
            {
                Console.WriteLine($"{wine.Name,30}");
            }
        }

        LineSeparator("[white]Red[/]");


        List<Wine> red = [.. context.Wines.Where(wine => wine.WineType == WineType.Red)];

        foreach (Wine wine in red)
        {
            Console.WriteLine($"{wine.Name,30}");
        }

    }

    #region Added by GitHub Copilot - Karen added static and SerializerOptions

    /// <summary>
    /// Serialize the list of wines to a JSON string
    /// </summary>
    /// <param name="wines">List of wines</param>
    /// <returns>JSON string</returns>
    public static string SerializeWines(List<Wine> wines)
    {
        return JsonSerializer.Serialize(wines, SerializerOptions);
    }

    /// <summary>
    /// Save the serialized JSON string to a file
    /// </summary>
    /// <param name="json">JSON string</param>
    /// <param name="filePath">File path to save the JSON string</param>
    public static void SaveJsonToFile(string json, string filePath)
    {
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Read the JSON data from a file and deserialize it into a list of wines
    /// </summary>
    /// <param name="filePath">File path to read the JSON string from</param>
    /// <returns>List of wines</returns>
    public static List<Wine>? ReadJsonFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return DeserializeWines(json);
    }

    /// <summary>
    /// Deserialize a JSON string to a list of wines
    /// </summary>
    /// <param name="json">JSON string</param>
    /// <returns>List of wines</returns>
    public static List<Wine>? DeserializeWines(string json)
    {
        return JsonSerializer.Deserialize<List<Wine>>(json);
    }

    #endregion

    /// <summary>
    /// Gets the JSON serializer options used for serializing and deserializing wine data.
    /// </summary>
    /// <value>
    /// A <see cref="JsonSerializerOptions"/> instance configured with specific settings, such as indented formatting.
    /// </value>
    public static JsonSerializerOptions SerializerOptions => new() { WriteIndented = true };
}
