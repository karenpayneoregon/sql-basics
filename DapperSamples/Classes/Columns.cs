#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DapperSamples.Classes;

public class Columns
{
    public string TableName { get; set; }
    public string ColumnName { get; set; }
    public int MaxLength { get; set; }
    public string DataType { get; set; }
    public int DataWidth { get; set; }

}