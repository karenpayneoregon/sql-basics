
using DapperBirthdaysComputedColumns.Classes;

namespace DapperBirthdaysComputedColumns;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        Setup();

        var table = CreateTable();
        var operations = new DapperOperations();
        var list = await operations.GetBirthdaysAsync();
        foreach (var bd in list)
        {
            table.AddRow(
                bd.Id.ToString(),
                bd.FirstName, 
                bd.LastName, 
                bd.BirthDate.ToString(), 
                bd.YearsOld.ToString());
        }

        AnsiConsole.Write(table);

        ExitPrompt();
    }

    public static Table CreateTable()
    {
        var table = new Table()
            .AddColumn("[b]Id[/]")
            .AddColumn("[b]First[/]")
            .AddColumn("[b]Last[/]")
            .AddColumn("[b]Birth date[/]")
            .AddColumn("[b]Age[/]")
            .Alignment(Justify.Left)
            .BorderColor(Color.LightSlateGrey);
        return table;
    }
}