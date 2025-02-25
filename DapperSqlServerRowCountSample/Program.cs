using DapperSqlServerRowCountSample.Classes;
using DapperSqlServerRowCountSample.Classes.Configurations;
using DapperSqlServerRowCountSample.Models;

using static DapperSqlServerRowCountSample.Classes.GeneralUtilities;

namespace DapperSqlServerRowCountSample;

/// <summary>
/// As presented
/// - Database connection string is read from appsettings.json
/// - The database is under localdb
///
/// If you are not using localdb, you can change the connection string in appsettings.json
/// and remove LocalDbDatabaseExists check.
/// </summary>
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();

        await GetRowCountsForTables();
        await GetRowCountsForSpecificTables();

        await WriteRecordCountsToFile();


        ExitPrompt();

    }

    /// <summary>
    /// Writes the record counts of specified database tables to a JSON file.
    /// </summary>
    /// <remarks>
    /// The method retrieves the row counts for the specified tables in the database
    /// and writes the data to a JSON file. The file name is derived from the initial catalog
    /// of the database connection string.
    /// </remarks>
    private static async Task WriteRecordCountsToFile()
    {
        await File.WriteAllTextAsync(
            $"{InitialCatalogFromConnectionString(DataConnections.Instance.MainConnection)}.json", 
            await GeneralUtilities.GetRecordCountAsJson(
                DataConnections.Instance.MainConnection,
                
                [
                    "Categories", 
                    "ContactDevices", 
                    "ContactType", 
                    "Countries", 
                    "PhoneType"
                ])
            );
    }

    /// <summary>
    /// Retrieves and displays the row counts for all tables in the database specified by the main connection string.
    /// </summary>
    /// <remarks>
    /// This method performs the following steps:
    /// <list type="bullet">
    /// <item>Prints the method name in cyan for logging purposes.</item>
    /// <item>Extracts the database name (InitialCatalog) from the main connection string.</item>
    /// <item>Checks if the specified LocalDB database exists.</item>
    /// <item>If the database exists:
    /// <list type="number">
    /// <item>Retrieves the row counts for all tables in the database.</item>
    /// <item>Displays the table information (schema, name, row count) in a formatted table.</item>
    /// <item>Indicates whether all tables have records or not.</item>
    /// </list>
    /// </item>
    /// <item>If the database does not exist, logs a failure message.</item>
    /// </list>
    /// </remarks>
    private static async Task GetRowCountsForTables()
    {
        PrintCyan();

        var catalog = InitialCatalogFromConnectionString(DataConnections.Instance.MainConnection);

        if (LocalDbDatabaseExists(catalog))
        {

            List<TableInfo> result = await TablesCount(DataConnections.Instance.MainConnection);

            var table = CreateViewTable($"All tables for {catalog}");

            foreach (var info in result)
            {
                table.AddRow(info.Schema, info.Name, info.RowCount.ToString());
            }

            AnsiConsole.Write(table);

            Console.WriteLine();

            AnsiConsole.MarkupLine(result.AllTablesHaveRecords()
                ? $"[cyan]All tables have records[/]"
                : $"[mediumvioletred]Not all tables have records[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[b]Failed to find[/][cyan] {DataConnections.Instance.MainConnection}[/]");
        }

        Console.WriteLine();

    }

    /// <summary>
    /// Retrieves and displays the row counts for a predefined set of specific tables
    /// in the database associated with the main connection string.
    /// </summary>
    /// <remarks>
    /// This method checks if the specified LocalDB database exists before proceeding.
    /// If the database exists, it retrieves the row counts for the specified tables,
    /// displays the results in a formatted table, and indicates whether all tables
    /// contain records. If the database does not exist, an error message is displayed.
    /// </remarks>
    private static async Task GetRowCountsForSpecificTables()
    {

        PrintCyan();

        var catalog = InitialCatalogFromConnectionString(DataConnections.Instance.MainConnection);

        if (LocalDbDatabaseExists(catalog))
        {
            string[] tableNames = ["Categories", "ContactDevices", "ContactType", "Countries", "PhoneType"];
            List<TableInfo> result = await GetTableRowCountsAsync(DataConnections.Instance.MainConnection, tableNames);

            var table = CreateViewTable($"Specific tables for {catalog}");

            foreach (var info in result)
            {
                table.AddRow(info.Schema, info.Name, info.RowCount.ToString());
            }

            AnsiConsole.Write(table);

            Console.WriteLine();

            AnsiConsole.MarkupLine(result.AllTablesHaveRecords()
                ? $"[cyan]All tables have records[/]"
                : $"[mediumvioletred]Not all tables have records[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[b]Failed to find[/][cyan] {DataConnections.Instance.MainConnection}[/]");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Creates a pre-configured table for displaying database table information.
    /// </summary>
    /// <param name="title">The name of the database catalog to be displayed in the table's title.</param>
    /// <returns>
    /// A <see cref="Table"/> object configured with a square border, centered alignment, 
    /// a title, and columns for schema, name, and row count.
    /// </returns>
    /// <remarks>
    /// The returned table is styled using Spectre.Console features, including custom borders, 
    /// colors, and column formatting.
    /// </remarks>
    private static Table CreateViewTable(string title) =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Grey100)
            .Alignment(Justify.Left)
            .Title($"[yellow][B]{title}[/][/]")
            .AddColumn(new TableColumn("[u]Schema[/]"))
            .AddColumn(new TableColumn("[u]Name[/]"))
            .AddColumn(new TableColumn("[u]Count[/]"));
}