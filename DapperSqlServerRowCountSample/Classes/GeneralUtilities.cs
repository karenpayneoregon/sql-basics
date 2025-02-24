using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DapperSqlServerRowCountSample.Models;

namespace DapperSqlServerRowCountSample.Classes;

/// <summary>
/// Provides utility methods for interacting with SQL Server databases, 
/// including retrieving database information, checking database existence, 
/// and fetching table row counts.
/// </summary>
class GeneralUtilities
{

    /// <summary>
    /// Get InitialCatalog from a valid connection string
    /// </summary>
    /// <param name="connectionString">Valid constructed connection string</param>
    /// <returns>InitialCatalog from <param name="connectionString">connectionString</param></returns>
    public static string InitialCatalogFromConnectionString(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).InitialCatalog;

    /// <summary>
    /// Checks whether a specified LocalDB database exists.
    /// </summary>
    /// <param name="databaseName">The name of the database to check.</param>
    /// <returns>
    /// <c>true</c> if the specified database exists; otherwise, <c>false</c>.
    /// </returns>
    public static bool LocalDbDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;integrated security=True;Encrypt=False");
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);

        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;

    }
    /// <summary>
    /// Retrieves a list of tables from the database specified by the connection string, 
    /// along with their respective row counts.
    /// </summary>
    /// <param name="connectionString">The connection string to the database.</param>
    /// <returns>A list of <see cref="TableInfo"/> objects, each containing the name and row count of a table.</returns>
    public static async Task<List<TableInfo>> TablesCount(string connectionString)
    {
        const string query =
            """
            SELECT 
                [Schema] = s.name,
                Name = t.name,
                [RowCount] = p.rows
            FROM sys.tables t
            INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
            INNER JOIN sys.indexes i ON t.object_id = i.object_id
            INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
            WHERE t.is_ms_shipped = 0
            GROUP BY t.name, s.name, p.rows
            ORDER BY s.name, t.name;
            """;

        await using var cn = new SqlConnection(connectionString);

        return (await cn.QueryAsync<TableInfo>(query)).ToList();
    }

    /// <summary>
    /// Asynchronously retrieves row count information for the specified tables in the database.
    /// </summary>
    /// <param name="connectionString">The connection string to the database.</param>
    /// <param name="tableNames">An array of table names for which to retrieve row count information.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a list of <see cref="TableInfo"/> objects,
    /// each representing a table with its schema, name, and row count.
    /// </returns>
    /// <remarks>
    /// This method queries the database using Dapper to fetch row count details for the specified tables.
    /// </remarks>
    /// <exception cref="SqlException">Thrown when there is an issue connecting to or querying the database.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="connectionString"/> or <paramref name="tableNames"/> is null.</exception>
    public static async Task<List<TableInfo>> GetTableRowCountsAsync(string connectionString, params string[] tableNames)
    {

        IDbConnection cn = new SqlConnection(connectionString);

        const string sql =
            """
                SELECT 
                    [Schema] = s.name,
                    Name = t.name,
                    [RowCount] = p.rows
                FROM sys.tables t
                INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                INNER JOIN sys.indexes i ON t.object_id = i.object_id
                INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
                WHERE t.is_ms_shipped = 0
                AND t.name IN @TableNames
                GROUP BY t.name, s.name, p.rows
                ORDER BY s.name, t.name;
            """;

        return (await cn.QueryAsync<TableInfo>(sql, new { TableNames = tableNames })).ToList();
    }
}