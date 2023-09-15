#nullable enable
using GetValuesApp.Classes;

namespace GetValuesApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await SqlOperations.WriteToFileAndConsoleForCommand();

        List<Person> persons = new List<Person>();
        var result = Examples.FindItemOrDefault(persons, "what ever") ?? new List<Person>()
        {
            new Person() { SomeProperty = "No records found"}
        };

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