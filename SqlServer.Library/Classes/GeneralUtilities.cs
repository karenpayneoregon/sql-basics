using System.Data;
using System.Diagnostics;
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
                   SELECT T.name TableName, i.Rows NumberOfRows 
                   FROM sys.tables T JOIN sys.sysindexes I ON T.OBJECT_ID = I.ID 
                   WHERE indid IN (0,1) 
                   ORDER BY i.Rows DESC,T.name
                   """, cn);

        DataTable table = new();
        cn.Open();

        table.Load(cmd.ExecuteReader());
        return table.AsEnumerable().All(row => row.Field<int>("NumberOfRows") > 0);

    }

    /// <summary>
    /// Determine the database has records
    /// </summary>
    /// <param name="databaseName">name of database</param>
    /// <returns>true if all tables have records</returns>
    public static bool ExpressDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;integrated security=True;Encrypt=False");
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);

        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;

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

    public static SqlConnectionEncryptOption EncryptSetting(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).Encrypt;

    public static bool LocalDbAvailable()
    {
        var (names, exception) = GetLocalDbServerNames();
        return exception is null && names.Length > 0 && names.Contains("MSSQLLocalDB", StringComparer.OrdinalIgnoreCase);
    }

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

    public static void ParseSqlLocalDbInfo()
    {
        try
        {
            // Start a new process to execute the sqllocaldb command
            Process process = new Process
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

            // Start the process
            process.Start();

            // Read the output
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            // Wait for the process to finish
            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                Debug.WriteLine("Error: " + error);
                return;
            }

            // Parse the output
            Debug.WriteLine("Parsing Results...");
            ParseOutput(output);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("An error occurred: " + ex.Message);
        }
    }

    private static void ParseOutput(string output)
    {
        LocalDbItem item = new();

        string[] lines = output.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (!line.Contains(":")) continue;
            var parts = line.Split([':'], 2);
            if (parts.Length != 2) continue;
            string key = parts[0].Trim();
            string value = parts[1].Trim();

            if (key == "Name")
            {
                item.Name = value;
            }else if (key == "Version")
            {
                item.Version = new Version(value);
            }else if (key == "Last start time")
            {
                item.LastStartTime = DateTime.Parse(value);
            }else if (key == "Owner")
            {
                item.Owner = value;
            }

            Debug.WriteLine($"{key}: {value}");
        }
    }
}