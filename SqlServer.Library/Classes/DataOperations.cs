using Dapper;
using Microsoft.Data.SqlClient;
using SqlServer.Library.Models;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace SqlServer.Library.Classes;
public class DataOperations
{
    /// <summary>
    /// Read all databases and tables from an instance of SQL-Server
    /// </summary>
    public static List<DataContainer> ReadDataContainers()
    {

        using var connection = new SqlConnection(ConnectionString());
        connection.Open();
        return connection.Query<DataContainer>(SqlStatements.GetDatabasesStatement).AsList();

    }
    /// <summary>
    /// Read view for current connection string in calling application
    /// </summary>
    /// <remarks>
    /// Other option is to pass in your own connection string
    /// </remarks>
    public static List<ViewContainer> ReadViews()
    {

        using var connection = new SqlConnection(ConnectionString());
        connection.Open();
        return connection.Query<ViewContainer>(SqlStatements.GetViewsStatement).AsList();

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
}
