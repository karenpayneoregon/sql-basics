namespace DapperSamples.Classes;
public class ColumnResult
{
    public string Value { get; set; }
    public int Length { get; set; }
}

public class Columns
{
    public string TableName { get; set; }
    public string ColumnName { get; set; }
    public int MaxLength { get; set; }
    public string DataType { get; set; }
    public int DataWidth { get; set; }

}