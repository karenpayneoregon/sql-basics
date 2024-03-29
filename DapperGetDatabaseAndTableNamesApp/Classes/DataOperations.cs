using Dapper;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace DapperGetDatabaseAndTableNamesApp.Classes;
internal class DataOperations
{
    public static List<DataContainer> ReadDataContainers()
    {
        using var connection = new SqlConnection(ConnectionString());
        connection.Open();
        return connection
            .Query<DataContainer>(SqlStatements.ReadStatement).AsList();

    }
}
