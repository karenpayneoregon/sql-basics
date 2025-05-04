using ConsoleConfigurationLibrary.Classes;
using SqlServerLibrary.Classes;

namespace DatabaseInspectorApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        var inspector = new DatabaseInspector(AppConnections.Instance.MainConnection);
        var catalog = DatabaseService.DatabaseName(AppConnections.Instance.MainConnection);
        
        var tableStats = inspector.GetTableSpaceUsages();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Centered()
            .Title($"[yellow]SQL Server Table Space Usage for [/] [{Color.Pink3}]{catalog}[/]")
            .AddColumn("[cyan]Schema[/]")
            .AddColumn("[cyan]Table[/]")
            .AddColumn("[cyan]Rows[/]")
            .AddColumn("[cyan]Total MB[/]")
            .AddColumn("[cyan]Used MB[/]")
            .AddColumn("[cyan]Unused MB[/]");

        foreach (var stat in tableStats)
        {
            table.AddRow(
                stat.SchemaName,
                stat.TableName,
                stat.Rows.ToString("N0"),
                stat.TotalSpaceMB.ToString("N2"),
                stat.UsedSpaceMB.ToString("N2"),
                stat.UnusedSpaceMB.ToString("N2")
            );
        }

        AnsiConsole.Write(table);

        Console.ReadLine();
    }
}