namespace SqlServer.Library.Classes;

/// <summary>
/// Provides a centralized list of database and table names to be excluded from operations.
/// </summary>
/// <remarks>
/// This class contains predefined lists of database and table names that are excluded from processing.
/// These lists are used in various operations to filter out specific databases and tables.
/// </remarks>
public class Exclude
{
    /// <summary>
    /// Database names to exclude
    /// </summary>
    public static List<string> DatabaseNameList => ["master", "msdb"];
    /// <summary>
    /// Table names to exclude
    /// </summary>
    public static List<string> TableNameList => ["sysdiagrams"];
}
