#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SqlServerLibrary.Models;
public class TableRow
{
    /// <summary>
    /// Table name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Table schema
    /// </summary>
    public string TableSchema { get; set; }
    /// <summary>
    /// Current row count for table
    /// </summary>
    public int RowCount { get; set; }
    public string SchemaTableName => $"{TableSchema}.{Name}";
    public override string ToString() => Name;

}
