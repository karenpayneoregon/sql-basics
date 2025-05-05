// ReSharper disable InconsistentNaming
#nullable disable

namespace SqlServerLibrary.Models;
/// <summary>
/// Represents the space usage details of a table in a SQL Server database.
/// </summary>
/// <remarks>
/// This class provides information about a table's name, schema, row count, 
/// total space, used space, and unused space, measured in both kilobytes and megabytes.
/// </remarks>
public class TableSpaceUsage
{
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public long Rows { get; set; }
    public long TotalSpaceKB { get; set; }
    public decimal TotalSpaceMB { get; set; }
    public long UsedSpaceKB { get; set; }
    public decimal UsedSpaceMB { get; set; }
    public long UnusedSpaceKB { get; set; }
    public decimal UnusedSpaceMB { get; set; }
}