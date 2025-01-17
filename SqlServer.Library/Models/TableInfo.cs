
#nullable disable
namespace SqlServer.Library.Models;
/// <summary>
/// Represents information about a SQL Server table, including its name and row count.
/// </summary>
public class TableInfo
{
    public string TableName { get; set; }
    public int RowCount { get; set; }
}

