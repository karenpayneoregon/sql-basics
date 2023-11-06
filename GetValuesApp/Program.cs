#nullable enable
using GetValuesApp.Classes;
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace GetValuesApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await SqlOperations.WriteToFileAndConsoleForCommand(7);

        List<Person> persons = new List<Person>();
        var result = Examples.FindItemOrDefault(persons, "what ever") ?? new List<Person>()
        {
            new Person() { SomeProperty = "No records found"}
        }!;

        Console.ReadLine();
    }
}

public class Person
{
    public string SomeProperty { get; set; }
}

public class Examples
{
    public static List<T?> FindItemOrDefault<T>(List<T> items, string id)
    {
        return default;
    }
}