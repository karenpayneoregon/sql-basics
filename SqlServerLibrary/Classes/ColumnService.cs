using Microsoft.Data.SqlClient;
using SqlServerLibrary.Models;
using System.Data;
using Dapper;

namespace SqlServerLibrary.Classes;
public class ColumnService
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());

    /// <summary>
    /// Get column descriptions from a SQL-Server table
    /// </summary>
    /// <param name="tableName">existing table under database in connection string</param>
    public List<ColumnDescriptions> ColumnDetails(string tableName)
    {
        return _cn.Query<ColumnDescriptions>(SqlStatements.DescriptionStatement, new { TableName = tableName}).AsList();
    }


}
