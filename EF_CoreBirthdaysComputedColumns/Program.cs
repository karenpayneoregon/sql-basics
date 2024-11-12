
using EF_CoreBirthdaysComputedColumns.Classes;
using EF_CoreBirthdaysComputedColumns.Data;
using Microsoft.EntityFrameworkCore;

namespace EF_CoreBirthdaysComputedColumns;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();

        await foreach (var bd in YieldOperations.AgeExample())
        {
            Console.WriteLine($"{bd.FirstName,-15}{bd.YearsOld,-5}{bd.BirthDate.Value.Year}");
        }
        ExitPrompt();
    }

    private static async Task Normal()
    {
        var table = CreateTable();
        await using var context = new Context();
        var list = await context.BirthDays.ToListAsync();
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
