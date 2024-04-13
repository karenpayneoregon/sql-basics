using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Dapper;
using SqlServerLibrary.Models;

namespace SqlServerLibrary.Classes;


public class TableService
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());
    
    public List<string> TableNames()
    {
        SqlConnectionStringBuilder builder = new(ConnectionString());

        return _cn.Query<string>(SqlStatements.TableNamesForDatabase(builder.InitialCatalog)).AsList();
    }
    public async Task<List<TableConstraints>> GetAllTableConstraints(string initialCatalog)
    {
        SqlConnectionStringBuilder builder = new(ConnectionString())
        {
            InitialCatalog = initialCatalog
        };
        var test = builder.ConnectionString;
        Debug.WriteLine(test);
        using var cn = new SqlConnection(builder.ConnectionString);
        return ( await cn.QueryAsync<TableConstraints>(SqlStatements.TableConstraintsForDatabase)).AsList();
    }
}

public class DatabaseService
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());

    public async Task<List<string>> DatabaseNames()
    {
        return ( await _cn.QueryAsync<string>(SqlStatements.GetDatabaseNames)).AsList();
    }
}

