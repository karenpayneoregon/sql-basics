namespace DapperSqlServerRowCountSample.Classes.Configurations;
/// <summary>
/// Known connection strings
/// </summary>
public sealed class DataConnections
{
    private static readonly Lazy<DataConnections> Lazy = new(() => new DataConnections());
    public static DataConnections Instance => Lazy.Value;
    /// <summary>
    /// Gets or sets the primary connection string for the application.
    /// </summary>
    /// <remarks>
    /// This property is read from appsettings.json.
    /// </remarks>
    public string MainConnection { get; set; }
    public string SecondaryConnection { get; set; }
}
