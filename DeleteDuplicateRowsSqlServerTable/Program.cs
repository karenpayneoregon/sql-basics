using DeleteDuplicateRowsSqlServerTable.Classes;

namespace DeleteDuplicateRowsSqlServerTable;

internal partial class Program
{
    static void Main(string[] args)
    {
        var table = CreateTable();

        Operations operations = new();

        // flush current data, repopulate with ten records
        operations.Populate();
        var data = operations.GetAll();

        table.AddRow("[red]With duplications[/]");
        foreach (var person in data)
        {
            table.AddRow(person.Id.ToString(), person.FirstName, person.LastName, $"{person.BirthDay:MM/dd/yyyy}");
        }

        // Remove duplicates
        operations.RemoveDuplicates();

        // Get data without duplicates
        data = operations.GetAll();
        table.AddRow("[yellow]Without duplications[/]");
        foreach (var person in data)
        {
            table.AddRow(person.Id.ToString(), person.FirstName, person.LastName, $"{person.BirthDay:MM/dd/yyyy}");
        }

        AnsiConsole.Write(table);

        SpectreConsoleHelpers.ExitPrompt();
    }
}