using DapperSamples.Classes;

namespace DapperSamples;

internal class Program
{
    static async Task Main(string[] args)
    {
        var list = await DataOperations.ColumnNames("Customers");
        var statement = SqlStatements.MinMaxForColumn("Customers", "CompanyName");
        List<ColumnResult> columnResults = await DataOperations.GetColumnLengths(statement);
    }
}
