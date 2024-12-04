using DapperForAccessDatabase.Classes;
using DapperForAccessDatabase.Models;
using DapperForAccessDatabase.Repositories;

namespace DapperForAccessDatabase;

internal partial class Program
{
    /// <summary>
    /// Run each method separately
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Table1Operations table1Operations = new();
        table1Operations.Add(new Table1 { Column1 = "Some value" });
        PersonSamples();
        //CustomersSamples();

        AnsiConsole.MarkupLine("[yellow]Press ENTER to quit[/]");
        Console.ReadLine();
    }

    private static void PersonSamples()
    {
        PersonRepository repository = new();

        // add several people
        repository.AddRange(MockedData.People());

        // get person by primary key
        var firstPerson = repository.Get(1);
        
        Console.WriteLine(repository.UpdateBirthDate(1, new DateOnly(1960, 12, 12))
            ? "Update successful"
            : "Update failed");


        Person person1 = new()
        {
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateOnly(1980, 1, 1),
            Active = true
        };
        repository.Insert(person1);
        // show all records
        var people = repository.GetAll();
        foreach (var person in people)
        {
            Console.WriteLine(person);
        }
    }

    private static void CustomersSamples()
    {
        CustomerRepository repository = new("Customers");

        var bogusCustomerList = BogusOperations.CustomersListHasNoIdentifiers(10_000);
        AnsiConsole.MarkupLine("[cyan]Bogus data generated[/] [yellow]Adding records...[/]");
        repository.Add(bogusCustomerList);
        var customers = repository.GetAll();
        Console.WriteLine(ObjectDumper.Dump(customers));
        AnsiConsole.MarkupLine($"[cyan]Record count {customers.Count}[/]");
    }
}