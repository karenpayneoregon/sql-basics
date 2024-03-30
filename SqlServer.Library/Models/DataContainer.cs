#nullable disable
namespace SqlServer.Library.Models;
public class DataContainer
{
    public string DatabaseName { get; set; }
    public string SchemaName { get; set; }
    public string TableName { get; set; }
    public override string ToString() => DatabaseName;
}