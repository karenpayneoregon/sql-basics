using Microsoft.Data.SqlClient;
using SqlServerLibrary.Models;
using Dapper;

namespace SqlServerLibrary.Classes;
/// <summary>
/// Provides functionality to inspect and retrieve information about a SQL Server database.
/// </summary>
/// <remarks>
/// This class is designed to interact with a SQL Server database using a provided connection string.
/// It includes methods to query and analyze database objects, such as tables, and retrieve detailed
/// information about their structure and usage.
/// </remarks>
public class DatabaseInspector(string connectionString)
{
    /// <summary>
    /// Retrieves the space usage details for all tables in the database.
    /// </summary>
    /// <remarks>
    /// This method executes a SQL query to fetch information about table space usage,
    /// including details such as table name, schema name, row count, total space, used space,
    /// and unused space in both kilobytes and megabytes.
    /// </remarks>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="TableSpaceUsage"/> objects, 
    /// where each object contains the space usage details for a specific table.
    /// </returns>
    public IEnumerable<TableSpaceUsage> GetTableSpaceUsages()
    {
        using var connection = new SqlConnection(connectionString);
        return connection.Query<TableSpaceUsage>(SqlStatements.TableUsage);
    }
}
