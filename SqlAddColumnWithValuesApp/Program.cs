using SqlAddColumnWithValuesApp.Classes;

namespace SqlAddColumnWithValuesApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        SqlOperations operations = new();
        AnsiConsole.MarkupLine($"[cyan]Has PhoneNumber?[/] [green]{operations.ContainsPhoneNumberColumn().ToYesNo()}[/]");
        operations.DropColumn();
        AnsiConsole.MarkupLine($"[cyan]Has PhoneNumber?[/] [green]{operations.ContainsPhoneNumberColumn().ToYesNo()}[/]");
        operations.AddColumn();
        AnsiConsole.MarkupLine($"[cyan]Has PhoneNumber?[/] [green]{operations.ContainsPhoneNumberColumn().ToYesNo()}[/]");

        var list = operations.GetAll();

        foreach (var detail in list)
        {
            Console.WriteLine($"{detail.Id,-4}{detail.LastName,-15}{detail.PhoneNumber}");
        }
        SpectreConsoleHelpers.ExitPrompt();
    }
}