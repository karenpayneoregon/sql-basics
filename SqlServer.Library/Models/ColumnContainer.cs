#nullable disable
namespace SqlServer.Library.Models;

public class ColumnContainer
{
    public int Position { get; set; }
    public string ColumnName { get; set; }
    public bool IsPrimaryKey { get; set; }
    public string DataTypeFull { get; set; }
    public bool IsComputed { get; set; }
}