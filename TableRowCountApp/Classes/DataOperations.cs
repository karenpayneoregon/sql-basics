using Microsoft.Data.SqlClient;
using Dapper;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Models;

namespace TableRowCountApp.Classes;
internal class DataOperations
{
    public static string DatabaseConnectionString(string catalog)
        => $"Data Source=.\\SQLEXPRESS;Initial Catalog={catalog};Integrated Security=True;Encrypt=False";

    public static List<string> DatabaseNames()
    {
        using SqlConnection cn = new("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Encrypt=False");
        return cn.Query<string>(SqlStatements.GetDatabaseNames).AsList();
    }

    public static List<TableRow> TableRowCount(string databaseName)
    {
        using SqlConnection cn = new(DatabaseConnectionString(databaseName));
        return cn.Query<TableRow>(SqlStatements.DatabaseTablesRowCount1).AsList();
    }
}
