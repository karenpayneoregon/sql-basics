#nullable disable

namespace SqlServer.Library.Models;
public class ForeignKeyContainer
{
    public string TableName { get; set; }
    public string ConstraintName { get; set; }
    public string ColumnName { get; set; }
    public string ForeignTableName { get; set; }
    public string ForeignColumnName { get; set; }
    public string UpdateRule { get; set; }
    public string DeleteRule { get; set; }

    public override string ToString() => TableName;

}
