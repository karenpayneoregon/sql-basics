
namespace SqlServerLibrary.Models;

public class TableConstraints
{
    public string? PrimaryKeyTable { get; set; }
    public string? ConstraintName { get; set; }
    public string? PrimaryKeyColumn { get; set; }
    public string? ForeignKeyTable { get; set; }
    public string? ForeignKeyColumn { get; set; }
    public string? UpdateRule { get; set; }
    public string? DeleteRule { get; set; }
    public override string ToString() => PrimaryKeyTable;
}
