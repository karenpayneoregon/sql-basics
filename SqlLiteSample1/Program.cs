using SqlLiteSample1.Classes;
using SqlLiteSample1.Models;
using static SqlLiteSample1.SpectreConsoleHelpers;

namespace SqlLiteSample1;

internal partial class Program
{

    static void Main(string[] args)
    {
        DapperOperations.CreateDatabase();
        AnsiConsole.MarkupLine("[cyan]Database created[/]");
        DapperOperations.CreateTableWithTwoRecords();
        AnsiConsole.MarkupLine("[cyan]Table created[/]");
        Console.WriteLine();

        AnsiConsole.MarkupLine("[cyan]Records[/]");
        DapperOperations.GetPeople();
        Console.WriteLine();

        List<Person> people =
        [
            new Person { FirstName = "John", LastName = "Doe", Pin = 12334 },
            new Person { FirstName = "Mary", LastName = "Adams", Pin = 55555 },
            new Person { FirstName = "Karen", LastName = "Payne", Pin = 77334 },
            new Person { FirstName = "Jane", LastName = "Doe", Pin = 56778 }
        ];

        DapperOperations.AddRange(people);

        AnsiConsole.MarkupLine("[cyan]Records[/]");
        DapperOperations.GetPeople();
        Console.WriteLine();

        var pin = DapperOperations.GetPin(1);
        AnsiConsole.MarkupLine($"[yellow]Pin[/] [cyan]{pin}[/]");

        DapperOperations.SetPin(1, 44444);

        pin = DapperOperations.GetPin(1);
        AnsiConsole.MarkupLine($"[yellow]Pin[/] [cyan]{pin}[/]");

        ExitPrompt();
    }


}