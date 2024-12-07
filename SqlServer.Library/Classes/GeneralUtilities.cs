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

    public static (bool success, Exception exception) ParseSqlLocalDbInfo()
    {

        LocalDbItem item = new();

        try
        {
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

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                return (false, new Exception(error));
            }

            ParseOutput(output);

        }
        catch (Exception ex)
        {
            return (false, ex);
        }

        return (true, null)!;
    }

    private static LocalDbItem ParseOutput(string output)
    {
        LocalDbItem item = new();

        string[] lines = output.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);

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
            }

            //Debug.WriteLine($"{key}: {value}");
        }

        return item;
    }
}