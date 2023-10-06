using GitHubSamples.Classes;
using GitHubSamples.Models;

namespace GitHubSamples;

internal partial class Program
{
    static async Task Main(string[] args)
    {


        await DataOperations.GetCategories();
        await StandardSample();
        await DataSetSample();
        DataSetSampleForumPost();
        await DapperSample();
        await DapperSampleStoredProcedure();

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

    private static async Task DapperSampleStoredProcedure()
    {
        ReferenceTables referenceTables = new();
        await DataOperations.GetReferenceTablesDapperStoredProcedure(referenceTables);
    }
    private static async Task DataSetSample()
    {
        var (success, exception, dataSet) = await DataOperations.GetReferenceTablesDataSet();
        Console.WriteLine(success
            ? "Success reading to DataSet"
            : $"DataSet operation failed with \n{exception.Message}");
    }

    private static void DataSetSampleForumPost()
    {
        var (success, exception, dataSet) = DataOperations.GetReferenceTablesDataSet1();
        Console.WriteLine(success
            ? "Success reading to DataSet1"
            : $"DataSet operation failed with \n{exception.Message}");
    }
}