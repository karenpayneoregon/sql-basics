using Dapper;
using Microsoft.Data.SqlClient;
using SqlServer.Library.Models;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

namespace SqlServer.Library.Classes;
public class DataOperations
{
    #region Two different ways to check if a catalog exists
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
    /// Determine if a database exists - using Dapper
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
    /// Reads all databases and their associated tables from an instance of SQL Server.
    /// </summary>
    /// <returns>
    /// A list of <see cref="DataContainer"/> objects, where each object represents a database, schema, and table.
    /// </returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that retrieves the database and table information.
    /// </remarks>
    public static List<DataContainer> ReadDataContainers()
    {
        using var connection = new SqlConnection(ConnectionString());
        return connection.Query<DataContainer>(SqlStatements.GetDatabasesStatement).AsList();
    }

    /// <summary>
    /// Asynchronously retrieves a list of all databases and their tables from an instance of SQL Server.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a list of <see cref="DataContainer"/> objects,
    /// each representing a database, schema, and table.
    /// </returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that retrieves database and table information.
    /// Ensure the connection string is properly configured before calling this method.
    /// </remarks>
    public static async Task<List<DataContainer>> ReadDataContainersAsync()
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<DataContainer>(SqlStatements.GetDatabasesStatement)).AsList();
    }

    /// <summary>
    /// Asynchronously retrieves all databases and their tables from an instance of SQL Server, grouped by database names.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a list of <see cref="IGrouping{TKey, TElement}"/> objects,
    /// where each group represents a database and contains a collection of <see cref="DataContainer"/> objects for its tables.
    /// </returns>
    /// <remarks>
    /// This method internally calls <see cref="ReadDataContainersAsync"/> to fetch the data and then groups it by database names.
    /// Ensure the connection string is properly configured before calling this method.
    /// </remarks>
    public static async Task<List<IGrouping<string, DataContainer>>> ReadDataContainersGroupedAsync()
    {
        var list = (await ReadDataContainersAsync()).AsList();
        return list.GroupBy(x => x.DatabaseName).ToList();
    }

    /// <summary>
    /// Reads all databases and their associated tables from an instance of SQL Server, 
    /// grouped by database names.
    /// </summary>
    /// <returns>
    /// A list of groupings, where each grouping contains a database name as the key 
    /// and a collection of <see cref="DataContainer"/> objects as the value.
    /// </returns>
    /// <remarks>
    /// This method uses LINQ to group the results of <see cref="ReadDataContainers"/> 
    /// by database names.
    /// </remarks>
    public static List<IGrouping<string, DataContainer>> ReadDataContainersGrouped()
        => ReadDataContainers().GroupBy(x => x.DatabaseName).ToList();


    /// <summary>
    /// Retrieves a list of views from the current database connection.
    /// </summary>
    /// <remarks>
    /// By default, the method uses the connection string configured in the calling application.
    /// Alternatively, you can provide a custom connection string.
    /// </remarks>
    /// <returns>
    /// A list of <see cref="ViewContainer"/> objects, each representing a view in the database.
    /// </returns>
    public static List<ViewContainer> ReadViews()
    {
        using var connection = new SqlConnection(ConnectionString());
        return connection.Query<ViewContainer>(SqlStatements.GetViewsStatement).AsList();
    }

    /// <summary>
    /// Retrieves a list of foreign key constraints for a specified database catalog.
    /// </summary>
    /// <param name="dataSource">The name of the server hosting the database.</param>
    /// <param name="catalog">The name of the database catalog to query.</param>
    /// <returns>A list of <see cref="ForeignKeyContainer"/> objects representing the foreign key constraints in the specified catalog.</returns>
    /// <remarks>
    /// This method uses a SQL query to fetch foreign key details, including table names, column names, 
    /// foreign table names, and update/delete rules. Only foreign keys with cascade rules are included.
    /// </remarks>
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
    /// Retrieves detailed information about the columns of a specified table.
    /// </summary>
    /// <param name="dataSource">The SQL Server instance to connect to.</param>
    /// <param name="catalog">The database (catalog) containing the table.</param>
    /// <param name="tableName">The name of the table for which column details are retrieved.</param>
    /// <returns>A list of <see cref="SqlServer.Library.Models.ColumnContainer"/> objects representing the column details of the specified table.</returns>
    /// <remarks>
    /// This method uses Dapper to execute a query that retrieves column metadata from the `INFORMATION_SCHEMA.COLUMNS` view.
    /// </remarks>
    /// <exception cref="SqlException">Thrown if there is an issue connecting to the SQL Server or executing the query.</exception>
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
    /// Asynchronously retrieves column details for a specified table in a SQL Server database.
    /// </summary>
    /// <param name="dataSource">The SQL Server instance to connect to.</param>
    /// <param name="catalog">The name of the database (Initial Catalog).</param>
    /// <param name="tableName">The name of the table to retrieve column details for.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="ColumnContainer"/> objects representing the column details.</returns>
    /// <exception cref="SqlException">Thrown when there is an issue connecting to the SQL Server or executing the query.</exception>
    /// <remarks>
    /// This method uses Dapper to execute a query that retrieves column details from the INFORMATION_SCHEMA.COLUMNS view.
    /// </remarks>
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
    /// Retrieves a list of database names available under the master database synchronously.
    /// </summary>
    /// <remarks>
    /// This method connects to the SQL Server using the connection string provided by the 
    /// <c>ConnectionString()</c> method and executes a query to fetch database names.
    /// </remarks>
    /// <returns>A list of database names as strings.</returns>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue connecting to the database or executing the query.
    /// </exception>
    /// <example>
    /// <code>
    /// var databaseNames = DataOperations.DatabaseNames();
    /// foreach (var name in databaseNames)
    /// {
    ///     Console.WriteLine(name);
    /// }
    /// </code>
    /// </example>
    public static List<string> DatabaseNames()
    {
        using var connection = new SqlConnection(ConnectionString());
        return connection.Query<string>(SqlStatements.GetDatabaseNames).AsList();
    }

    /// <summary>
    /// Asynchronously retrieves a list of database names available under the master database.
    /// </summary>
    /// <remarks>
    /// This method connects to the SQL Server using the connection string provided by the 
    /// <c>ConnectionString</c> method and executes a query to fetch the database names.
    /// </remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains a list of database names.</returns>
    /// <exception cref="SqlException">Thrown when there is an issue connecting to the database or executing the query.</exception>
    /// <example>
    /// The following example demonstrates how to use this method:
    /// <code>
    /// var databaseNames = await DataOperations.DatabaseNamesAsync();
    /// foreach (var name in databaseNames)
    /// {
    ///     Console.WriteLine(name);
    /// }
    /// </code>
    /// </example>
    public static async Task<List<string>> DatabaseNamesAsync()
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<string>(SqlStatements.GetDatabaseNames)).AsList();
    }

    /// <summary>
    /// Asynchronously retrieves a list of table names for the specified database.
    /// </summary>
    /// <param name="databaseName">The name of the database to retrieve table names from.</param>
    /// <returns>A task representing the asynchronous operation, containing a list of table names.</returns>
    public static async Task<List<string>> TableNamesAsync(string databaseName)
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await connection.QueryAsync<string>(SqlStatements.TableNamesForDatabase(databaseName))).AsList();
    }

    /// <summary>
    /// Retrieves a list of table names from the specified database.
    /// </summary>
    /// <param name="databaseName">The name of the database to query for table names.</param>
    /// <returns>A list of table names within the specified database.</returns>
    public static List<string> TableNames(string databaseName)
    {
        using var connection = new SqlConnection(ConnectionString());
        return (connection.Query<string>(SqlStatements.TableNamesForDatabase(databaseName))).AsList();
    }

    /// <summary>
    /// Asynchronously retrieves a list of table names for the specified database, 
    /// each paired with a corresponding index.
    /// </summary>
    /// <param name="databaseName">The name of the database to retrieve table names from.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a list of 
    /// <see cref="TableNameIndexContainer"/> objects. Each object contains the table name 
    /// and its associated index.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="TableNamesAsync(string)"/> method to fetch table names 
    /// and pairs each name with an index to create a list of <see cref="TableNameIndexContainer"/> objects.
    /// </remarks>
    public static async Task<List<TableNameIndexContainer?>> TableNamesWithIndicesAsync(string databaseName)
    {
        await using var connection = new SqlConnection(ConnectionString());
        return (await TableNamesAsync(databaseName)).Select((name, index) => new TableNameIndexContainer(index, name))
            .ToList();
    }

    /// <summary>
    /// Retrieves a list of table names from the specified database, each paired with a mock index.
    /// </summary>
    /// <param name="databaseName">The name of the database to query for table names.</param>
    /// <returns>
    /// A list of <see cref="TableNameIndexContainer"/> objects, where each object contains a table name and its associated index.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="TableNames"/> method to fetch table names and assigns a sequential index to each table.
    /// </remarks>
    public static List<TableNameIndexContainer> TableNamesWithIndices(string databaseName)
    {
        using var connection = new SqlConnection(ConnectionString());
        return (TableNames(databaseName))
            .Select((name, index) => new TableNameIndexContainer(index, name))
            .ToList();
    }
}