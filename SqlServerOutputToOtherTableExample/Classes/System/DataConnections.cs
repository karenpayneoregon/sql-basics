namespace SqlServerOutputToOtherTableExample.Classes.System;
/// <summary>
/// Known connection strings
/// </summary>
public sealed class DataConnections
{
    private static readonly Lazy<DataConnections> Lazy = new(() => new DataConnections());
    public static DataConnections Instance => Lazy.Value;
    /// <summary>
    /// Gets or sets the connection string for the main database from appsettings.json.
    /// </summary>
    /// <remarks>
    /// This property is used to establish a connection to the primary database.
    /// It is typically configured during application setup by reading from configuration settings.
    /// </remarks>
    public string MainConnection { get; set; }
}
