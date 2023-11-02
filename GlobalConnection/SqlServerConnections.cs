using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;

namespace GlobalConnection;
public sealed class SqlServerConnections
{
    private static readonly Lazy<SqlServerConnections>
        Lazy = new(() => new SqlServerConnections());

    public string ConnectionString = 
        """
        Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NorthWind2022;Integrated Security=True;Encrypt=False
        """;
    public static SqlServerConnections Instance => Lazy.Value;

    private readonly Hashtable _connections = new();

    /// <summary>
    /// Returns an open connection for connection string
    /// </summary>
    /// <returns>Connection</returns>
    public SqlConnection Connection()
    {
        SqlConnection connection = null;
        var bNeedAdd = false;

        try
        {
            connection = (SqlConnection)_connections[ConnectionString];
        }
        catch (Exception localException)
        {
            // ignored for this example but for a real app log the error
        }

        if (connection == null)
        {
            bNeedAdd = true;
        }

        if (connection == null || 
            connection.State == ConnectionState.Broken || 
            connection.State == ConnectionState.Closed)
        {
            try
            {
                connection?.Dispose();
                connection = null;
            }
            catch (Exception)
            {
                // ignored
            }

            connection = new SqlConnection();
        }

        if (connection.State == ConnectionState.Closed)
        {
            connection.ConnectionString = ConnectionString;
            connection.Open();
        }

        if (bNeedAdd)
        {
            _connections.Add(ConnectionString, connection);
        }

        return connection;
    }

}
