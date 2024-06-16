namespace SqlServerOutputToOtherTableExample.Classes;
/// <summary>
/// Known connection strings
/// </summary>
public sealed class DataConnections
{
    private static readonly Lazy<DataConnections> Lazy = new(() => new DataConnections());
    public static DataConnections Instance => Lazy.Value;
    /// <summary>
    /// Connection strings from appsettings.json
    /// </summary>
    public string MainConnection { get; set; }
}
