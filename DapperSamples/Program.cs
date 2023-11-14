using DapperSamples.Classes;
using Spectre.Console;

namespace DapperSamples;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        var list = await DataOperations.ColumnNames("Customers");

        var statement = SqlStatements.MinMaxForColumn("Customers", "CompanyName");
        List<ColumnResult> columnResults = await DataOperations.GetColumnLengths(statement);

        var columnsList = await DataOperations.GetColumnLengths1("Customers");
        Console.WriteLine();
        AnsiConsole.MarkupLine("  [cyan]Column name              Len       Type      Width[/]");
        foreach (var col in columnsList)
        {
            Console.WriteLine($"  {col.ColumnName,-25}{col.MaxLength,-10}{col.DataType,-10}{col.DataWidth}");
        }

        Console.ReadLine();
    }
}
