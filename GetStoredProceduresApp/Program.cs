using System.Text;
using GetStoredProceduresApp.Classes;
using SqlServerLibrary.Classes;

namespace GetStoredProceduresApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        
        StringBuilder builder = new();
        StoredProcedureHelpers helpers = new();
        var names = helpers.GetStoredProcedureName();
        foreach (var name in names)
        {
            AnsiConsole.MarkupLine($"[yellow]Processing[/] [white]{name}[/]");
            var definition = helpers.GetStoredProcedureDefinition(name);
            builder.AppendLine($"Procedure name: {name}");
            builder.AppendLine(definition);
            builder.AppendLine(new string('-',200));
        }

        var fileName = "StoredProcedures.txt";
        File.WriteAllText(fileName, builder.ToString());
        AnsiConsole.MarkupLine($"[cyan]Result written to[/] [white]{fileName}[/] [cyan]for [/][white]{names.Count}[/][cyan] procedures[/]");
        SpectreConsoleHelpers.ExitPrompt();
    }
}