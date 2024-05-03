using SqlLiteInsertNulls.Classes;

namespace SqlLiteInsertNulls;

internal partial class Program
{
    static void Main(string[] args)
    {
        var count = DapperOperations.InsertData(MockedData.GetMockedData());
        
        AnsiConsole.MarkupLine($"[yellow]Records added[/] {count}");
        Console.ReadLine();
    }
}