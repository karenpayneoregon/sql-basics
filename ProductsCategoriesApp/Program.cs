using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ProductsCategoriesApp.Classes;
using ProductsCategoriesApp.Data;

namespace ProductsCategoriesApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        var results = await Operations.GetContactsWithDevicesPhoneTypeIdentifierNavigation();
        List<int> identifiers = new List<int>();
        foreach (var result in results)
        {
            //identifiers.Add(result.CustomerIdentifier);
        }
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(results, options);
        string fileName = "results.json";
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        await File.WriteAllTextAsync(fileName, jsonString);
        var missing = identifiers.Missing();


        //Console.ReadLine();
    }

    private static async Task EntityCode()
    {
        await using var context = new Context();
        var customers = context.Customers
            .Include(c => c.CountryIdentifierNavigation)
            .Include(c => c.Contact)
            .ThenInclude(c => c.ContactTypeIdentifierNavigation).ToList();
    }
}

public static class SequenceExtensions
{
    /// <summary>
    /// Find missing elements in a list of int
    /// </summary>
    /// <param name="sequence">int array which may have gaps</param>
    /// <returns>gap array or an empty array</returns>
    public static int[] Missing(this List<int> sequence)
    {
        sequence.Sort();

        return Enumerable
            .Range(1, sequence[^1])
            .Except(sequence)
            .ToArray();
    }
    public static int[] Missing(this int[] sequence)
    {
        //Array.Sort(sequence.ToArray());
        return Enumerable
            .Range(1, sequence[^1])
            .Except(sequence)
            .ToArray();
    }

}