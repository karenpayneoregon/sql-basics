using Dapper;
using Microsoft.Data.SqlClient;
using SqlServer.Library.Models;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

namespace SqlServer.Library.Classes;
public class DataOperations
{
    #region Two ifferent ways to check if a catalog exists
    /// <summary>
    /// Determine if a database exists
    /// </summary>
    /// <param name="catalog">database name</param>
    /// <returns>If the catalog exists</returns>
    public static bool CatalogExists(string catalog)
    {
        using var connection = new SqlConnection(ConnectionString());
        const string sql = "SELECT name FROM sys.databases WHERE name = @DatabaseName;";
        return connection.QueryFirstOrDefault<string>(sql, new { DatabaseName = catalog }) != null;
    }

    /// <summary>
    /// Determine if a database exists
    /// </summary>
    /// <param name="catalog">database name</param>
    /// <returns>If the catalog exists</returns>
    public static bool CatalogExists1(string catalog)
    {
        using var connection = new SqlConnection(ConnectionString());
        const string sql =
            """
            IF EXISTS (SELECT name FROM sys.databases WHERE name = @DatabaseName)
                SELECT 1;
            ELSE
                SELECT 0;
            """;
        return connection.QueryFirstOrDefault<int>(sql, new { DatabaseName = catalog }) == 1;
    }
    #endregion

    /// <summary>
    /// Read all databases and tables from an instance of SQL-Server
    /// </summary>
    public static List<DataContainer> ReadDataContainers()
    {

        using var connection = new SqlConnection(ConnectionString());
        return connection.Query<DataContainer>(SqlStatements.GetDatabasesStatement).AsList();

    }

    /// <summary>
    /// Read all databases and tables from an instance of SQL-Server
    /// </summary>
    public static async Task<List<DataContainer>> ReadDataContainersAsync()
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<DataContainer>(SqlStatements.GetDatabasesStatement)).AsList();

    }

    /// <summary>
    /// Read all databases and tables from an instance of SQL-Server grouped by database names asynchronously
    /// </summary>
    /// <returns></returns>
    public static async Task<List<IGrouping<string, DataContainer>>> ReadDataContainersGroupedAsync()
    {
        var list = (await ReadDataContainersAsync()).AsList();
        return list.GroupBy(x => x.DatabaseName).ToList();
    }

    /// <summary>
    /// Read all databases and tables from an instance of SQL-Server grouped by database names
    /// </summary>
    /// <returns></returns>
    public static List<IGrouping<string, DataContainer>> ReadDataContainersGrouped()
        => ReadDataContainers().GroupBy(x => x.DatabaseName).ToList();


    /// <summary>
    /// Read view for current connection string in calling application
    /// </summary>
    /// <remarks>
    /// Other option is to pass in your own connection string
    /// </remarks>
    public static List<ViewContainer> ReadViews()
    {

        using var connection = new SqlConnection(ConnectionString());
        return connection.Query<ViewContainer>(SqlStatements.GetViewsStatement).AsList();

    }

    /// <summary>
    /// Get foreign keys for a catalog
    /// </summary>
    /// <param name="dataSource">server</param>
    /// <param name="catalog">database</param>
    /// <returns></returns>
    public static List<ForeignKeyContainer> ReadForeignKeys(string dataSource, string catalog)
    {
        SqlConnectionStringBuilder builder = new(ConnectionString())
        {
            DataSource = dataSource,
            InitialCatalog = catalog
        };

        using var connection = new SqlConnection(builder.ConnectionString);

        return connection.Query<ForeignKeyContainer>(SqlStatements.GetForeignKeysWithRules).AsList();

    }

    /// <summary>
    /// Get column details for a table
    /// </summary>
    /// <param name="dataSource">sql-server instance</param>
    /// <param name="catalog">Initial Catalog</param>
    /// <param name="tableName">table to get column details for</param>
    /// <returns></returns>
    public static List<ColumnContainer> ReadColumnDetailsForTable(string dataSource, string catalog, string tableName)
    {

        SqlConnectionStringBuilder builder = new(ConnectionString())
        {
            DataSource = dataSource,
            InitialCatalog = catalog
        };

        using var connection = new SqlConnection(builder.ConnectionString);
        return connection.Query<ColumnContainer>(SqlStatements.GetColumnDetailsForTable,
            new { TableName = tableName }).AsList();
    }

    /// <summary>
    /// Get column details for a table
    /// </summary>
    /// <param name="dataSource">sql-server instance</param>
    /// <param name="catalog">Initial Catalog</param>
    /// <param name="tableName">table to get column details for</param>
    /// <returns></returns>
    public static async Task<List<ColumnContainer>> ReadColumnDetailsForTableAsync(string dataSource, string catalog, string tableName)
    {

        SqlConnectionStringBuilder builder = new(ConnectionString())
        {
            DataSource = dataSource,
            InitialCatalog = catalog
        };

        await using var connection = new SqlConnection(builder.ConnectionString);
        return (await connection.QueryAsync<ColumnContainer>(SqlStatements.GetColumnDetailsForTable,
            new { TableName = tableName })).AsList();
    }

    /// <summary>
    /// Get database names under master synchronously
    /// </summary>
    /// <returns>List of database names</returns>
    public static List<string> DatabaseNames()
    {
        using var connection = new SqlConnection(ConnectionString());
        return connection.Query<string>(SqlStatements.GetDatabaseNames).AsList();
    }

    /// <summary>
    /// Get database names under master asynchronously
    /// </summary>
    /// <returns>List of database names</returns>
    public static async Task<List<string>> DatabaseNamesAsync()
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<string>(SqlStatements.GetDatabaseNames)).AsList();
    }

    /// <summary>
    /// Get table names for database
    /// </summary>
    /// <param name="databaseName">database to get details for</param>
    /// <returns>list of table names</returns>
    public static async Task<List<string>> TableNamesAsync(string databaseName)
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<string>(SqlStatements.TableNamesForDatabase(databaseName))).AsList();
    }

    /// <summary>
    /// Get table names for database
    /// </summary>
    /// <param name="databaseName">database to get details for</param>
    /// <returns>list of table names</returns>
    public static List<string> TableNames(string databaseName)
    {
        using var connection = new SqlConnection(ConnectionString());
        return (connection.Query<string>(SqlStatements.TableNamesForDatabase(databaseName))).AsList();
    }

    /// <summary>
    /// Get table names for a database with mocked indices
    /// </summary>
    /// <param name="databaseName">database to get details for</param>
    public static async Task<List<TableNameIndexContainer?>> TableNamesWithIndicesAsync(string databaseName)
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await TableNamesAsync(databaseName)).Select((name, index) => new TableNameIndexContainer(index, name))
            .ToList();
    }

    /// <summary>
    /// Get table names for a database with mocked indices
    /// </summary>
    /// <param name="databaseName">database to get details for</param>
    public static List<TableNameIndexContainer> TableNamesWithIndices(string databaseName)
    {
        using var connection = new SqlConnection(ConnectionString());
        return (TableNames(databaseName))
            .Select((name, index) => new TableNameIndexContainer(index, name))
            .ToList();
    }
}