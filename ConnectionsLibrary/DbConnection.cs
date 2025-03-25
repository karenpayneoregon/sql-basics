using System.Data;
using ConsoleConfigurationLibrary.Models;
using Microsoft.Data.SqlClient;
using static ConsoleConfigurationLibrary.Classes.Configuration;

namespace ConnectionsLibrary;

public static class DbConnection
{
    private static readonly Lazy<string> ConnectionString = new(LoadConnectionString);

    /// <summary>
    /// Loads the connection string for the main database connection.
    /// </summary>
    /// <returns>The connection string for the main database connection.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the main database connection string is missing in the configuration.
    /// </exception>
    private static string LoadConnectionString() =>
        JsonRoot().GetConnectionString(nameof(ConnectionStrings.MainConnection)) ?? 
        throw new InvalidOperationException($"Database connection string {nameof(ConnectionStrings.MainConnection)} is missing.");

    /// <summary>
    /// Creates and returns a new database connection instance.
    /// </summary>
    /// <returns>
    /// An <see cref="InvalidOperationException"/> instance initialized with the main database connection string.
    /// </returns>
    /// <remarks>
    /// The returned connection must be properly disposed of after use to release resources.
    /// </remarks>
    /// <exception cref="IDbConnection">
    /// Thrown if the main database connection string is not configured or is missing.
    /// </exception>
    public static IDbConnection CreateConnection() => new SqlConnection(ConnectionString.Value);
}



