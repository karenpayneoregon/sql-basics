using GitHubSamples.Classes;
using GitHubSamples.Models;

namespace GitHubSamples;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await StandardSample();
        await DataSetSample();
        await DapperSample();

        AnsiConsole.MarkupLine("[yellow]Press ENTER to exit[/]");
        Console.ReadLine();
    }

    private static async Task StandardSample()
    {
        ReferenceTables referenceTables = new();
        var (success, exception) = await DataOperations.GetReferenceTables(referenceTables);
        Console.WriteLine(success
            ? "Success reading to classes"
            : $"Class operation failed with \n{exception.Message}");
    }

    private static async Task DapperSample()
    {
        ReferenceTables referenceTables = new();
        await DataOperations.GetReferenceTablesDapper(referenceTables);
    }

    private static async Task DataSetSample()
    {
        var (success, exception, dataSet) = await DataOperations.GetReferenceTablesDataSet();
        Console.WriteLine(success
            ? "Success reading to DataSet"
            : $"DataSet operation failed with \n{exception.Message}");
    }
}