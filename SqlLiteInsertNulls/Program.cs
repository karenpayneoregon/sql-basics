using SqlLiteInsertNulls.Classes;

namespace SqlLiteInsertNulls;

internal partial class Program
{
    static void Main(string[] args)
    {
        var result = DapperOperations.InsertData(MockedData.GetMockedData());
        AnsiConsole.MarkupLine("[yellow]Hello[/]");
        Console.ReadLine();
    }
}