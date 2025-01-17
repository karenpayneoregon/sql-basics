using System.Data;
using System.Diagnostics;
using Dapper;
using Microsoft.Data.SqlClient;
using SqlServer.Library.Models;

namespace SqlServer.Library.Classes;

public class GeneralUtilities
{
    /// <summary>
    /// Checks if all tables in the database specified by the connection string are populated with records.
    /// </summary>
    /// <param name="connectionString">The connection string to the database.</param>
    /// <returns><c>true</c> if all tables have records; otherwise, <c>false</c>.</returns>
    public static bool TablesArePopulated(string connectionString)
    {
        using var cn = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(
            """
               SELECT     t.name AS TableName,
                          p.rows AS [RowCount]
                FROM      sys.tables t
               INNER JOIN sys.partitions p
                  ON t.object_id = p.object_id
               WHERE      p.index_id IN ( 0, 1 )
               ORDER BY p.rows DESC,
                        t.name;
               """, cn);

        DataTable table = new();
        cn.Open();

        table.Load(cmd.ExecuteReader());
        return table.AsEnumerable().All(row => row.Field<int>("[RowCount]") > 0);

    }
    /// <summary>
    /// Checks if all tables in the database specified by the connection string are populated with records.
    /// </summary>
    /// <param name="connectionString">The connection string to the database.</param>
    /// <returns><c>true</c> if all tables have records; otherwise, <c>false</c>.</returns>
    /// <remarks>Uses the Dapper library for querying the database.</remarks>
    public static bool TablesArePopulated1(string connectionString)
    {
        const string query = 
            """
            SELECT t.name AS TableName,
                   p.rows AS [RowCount]
            FROM sys.tables t
            INNER JOIN sys.partitions p
                ON t.object_id = p.object_id
            WHERE p.index_id IN (0, 1)
            ORDER BY p.rows DESC, t.name;
            """;

        using var cn = new SqlConnection(connectionString);

        var results = cn.Query(query).ToList();
        return results.All(row => (int)row.RowCount > 0);
    }

    /// <summary>
    /// Gets the connection string for the master database on the SQL Server Express instance.
    /// </summary>
    /// <returns>A connection string for the master database.</returns>
    private static string MasterConnectionString()
        => "Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Encrypt=False";


    /// <summary>
    /// Checks if a database with the specified name exists on the SQL Server Express instance.
    /// </summary>
    /// <param name="databaseName">The name of the database to check for existence.</param>
    /// <returns><c>true</c> if the database exists; otherwise, <c>false</c>.</returns>
    public static bool ExpressDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection(MasterConnectionString());
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);
        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;
    }

    /// <summary>
    /// Checks if a database with the specified name exists on the SQL Server Express instance.
    /// </summary>
    /// <param name="databaseName">The name of the database to check for existence.</param>
    /// <returns><c>true</c> if the database exists; otherwise, <c>false</c>.</returns>
    /// <remarks>Using Dapper NuGet package</remarks>
    public static bool ExpressDatabaseExists1(string databaseName)
    {
        using var cn = new SqlConnection(MasterConnectionString());
        return cn.QuerySingleOrDefault<int?>("SELECT DB_ID(@DatabaseName);", 
            new { DatabaseName = databaseName }).HasValue;
    }

    /// <summary>
    /// Determine the database has records
    /// </summary>
    /// <param name="databaseName">name of database</param>
    /// <returns>true if all tables have records</returns>
    public static bool LocalDbDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;integrated security=True;Encrypt=False");
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);

        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;

    }

    /// <summary>
    /// Get InitialCatalog from a valid connection string
    /// </summary>
    /// <param name="connectionString">Valid constructed connection string</param>
    /// <returns>InitialCatalog from <param name="connectionString">connectionString</param></returns>
    public static string InitialCatalogFromConnectionString(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).InitialCatalog;

    /// <summary>
    /// Get DataSource from a valid connection string
    /// </summary>
    /// <param name="connectionString">Valid constructed connection string</param>
    /// <returns>DataSource from <param name="connectionString">connectionString</param></returns>
    public static string DataSourceFromConnectionString(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).DataSource;

    /// <summary>
    /// Retrieves the encryption setting from the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string from which to extract the encryption setting.</param>
    /// <returns>The <see cref="SqlConnectionEncryptOption"/> indicating the encryption setting.</returns>
    public static SqlConnectionEncryptOption EncryptSetting(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).Encrypt;

    /// <summary>
    /// Checks if the LocalDB instance "MSSQLLocalDB" is available on the system.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the "MSSQLLocalDB" instance is available; otherwise, <c>false</c>.
    /// </returns>
    public static bool LocalDbAvailable()
    {
        var (names, exception) = GetLocalDbServerNames();
        return exception is null && names.Length > 0 && names.Contains("MSSQLLocalDB", StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Retrieves the names of available LocalDB server instances.
    /// </summary>
    /// <returns>
    /// A tuple containing an array of server instance names and an exception. 
    /// The array contains the names of the LocalDB server instances if successful; 
    /// otherwise, it is empty. The exception is <c>null</c> if the operation is successful; 
    /// otherwise, it contains the exception that occurred.
    /// </returns>
    public static (string[] names, Exception exception) GetLocalDbServerNames()
    {
        try
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "sqllocaldb",
                    Arguments = "i",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
                return ([], new InvalidOperationException($"Error executing sqllocaldb: {error}"));


            return (output.Split(Environment.NewLine).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray(), null)!;

        }
        catch (Exception generalException)
        {
            return ([], generalException);
        }
    }

    /// <summary>
    /// Retrieves information about the local SQL Server instance.
    /// </summary>
    /// <returns>
    /// A tuple containing a boolean indicating success, a <see cref="LocalDbItem"/> with the local database information, 
    /// and an <see cref="Exception"/> if an error occurred.
    /// </returns>
    public static (bool success, LocalDbItem dbItem, Exception exception) SqlLocalInfo()
    {

        LocalDbItem item = new();

        try
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "sqllocaldb",
                    Arguments = "info MSSQLLocalDB",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                return (false, item, new Exception(error));
            }

            item = ParseOutput(output);

        }
        catch (Exception ex)
        {
            return (false, item, ex);
        }

        return (true, item, null)!;
    }

    /// <summary>
    /// Parses the output from the SQL LocalDB command and extracts information into a <see cref="LocalDbItem"/>.
    /// </summary>
    /// <param name="output">The output string from the SQL LocalDB command.</param>
    /// <returns>A <see cref="LocalDbItem"/> populated with the parsed data.</returns>
    private static LocalDbItem ParseOutput(string output)
    {
        LocalDbItem item = new();

        var lines = output.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (!line.Contains(":")) continue;

            var parts = line.Split([':'], 2);

            if (parts.Length != 2) continue;

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            switch (key)
            {
                case "Name":
                    item.Name = value;
                    break;
                case "Version":
                    item.Version = new Version(value);
                    break;
                case "Last start time":
                    item.LastStartTime = DateTime.Parse(value);
                    break;
                case "Owner":
                    item.Owner = value;
                    break;
                case "Auto-create":
                    item.AutoCreate = value.ToLower() == "yes";
                    break;
                case "State":
                    item.State = value;
                    break;
                case "Instance pipe name":
                    item.InstancePipeName = value;
                    break;
            }

            if (Debugger.IsAttached)
            {
                Debug.WriteLine($"{key}: {value}");
            }

        }

        return item;
    }
}