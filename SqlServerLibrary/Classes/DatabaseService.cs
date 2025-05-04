using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SqlServerLibrary.Classes;

public class DatabaseService
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());

    /// <summary>
    /// Retrieves a list of database names from the SQL Server instance.
    /// </summary>
    /// <remarks>
    /// The method queries the SQL Server instance for all accessible databases,
    /// excluding system databases (e.g., master, tempdb, model, msdb) and databases
    /// with names starting with 'ReportServer'.
    /// </remarks>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a list of database names.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue executing the SQL query.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the connection is not properly initialized or opened.
    /// </exception>
    public async Task<List<string>> DatabaseNames()
    {
        return ( await _cn.QueryAsync<string>(SqlStatements.GetDatabaseNames)).AsList();
    }

    /// <summary>
    /// Extracts the database name from a given SQL Server connection string.
    /// </summary>
    /// <param name="connectionString">
    /// The connection string from which the database name will be extracted.
    /// </param>
    /// <returns>
    /// The name of the database specified in the connection string.
    /// </returns>
    public static string DatabaseName(string connectionString) => new SqlConnectionStringBuilder(connectionString).InitialCatalog;
}