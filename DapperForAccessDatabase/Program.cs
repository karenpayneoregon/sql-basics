using DapperForAccessDatabase.Classes;
using DapperForAccessDatabase.Repositories;

namespace DapperForAccessDatabase;

internal partial class Program
{
    static void Main(string[] args)
    {
        CustomerRepository repository = new();

        var bogusCustomerList = BogusOperations.CustomersListHasNoIdentifiers(10000);
        AnsiConsole.MarkupLine("[cyan]Bogus data generated[/] [yellow]Adding records...[/]");
        repository.Add(bogusCustomerList);
        var customers = repository.GetAll();
        Console.WriteLine(ObjectDumper.Dump(customers));
        AnsiConsole.MarkupLine($"[cyan]Record count {customers.Count}[/]");
        AnsiConsole.MarkupLine("[yellow]Press ENTER to quit[/]");
        Console.ReadLine();
    }
}