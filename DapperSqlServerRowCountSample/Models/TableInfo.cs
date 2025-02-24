
#nullable disable
namespace DapperSqlServerRowCountSample.Models;
/// <summary>
/// Represents information about a SQL Server table, including its schema, name, and row count.
/// </summary>
public class TableInfo
{
    public string Schema { get; set; }
    public string Name { get; set; }
    public int RowCount { get; set; }
}

