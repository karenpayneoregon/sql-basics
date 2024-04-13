using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SqlServerLibrary.Classes;

public class DatabaseService
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());

    public async Task<List<string>> DatabaseNames()
    {
        return ( await _cn.QueryAsync<string>(SqlStatements.GetDatabaseNames)).AsList();
    }
}