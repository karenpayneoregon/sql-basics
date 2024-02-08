using Dapper;
using kp.Dapper.Handlers;
using Microsoft.Data.SqlClient;
using SqlAddColumnWithValuesApp.Models;
using System.Data;

namespace SqlAddColumnWithValuesApp.Classes;
internal class SqlOperations
{
    private IDbConnection _cn;

    public SqlOperations()
    {
        _cn = new SqlConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }
    public List<UserDetail> GetAll()
        => _cn.Query<UserDetail>(SqlStatements.SelectStatement).ToList();

    public void DropColumn()
    {
        _cn.Execute(SqlStatements.DropPhoneNumberColumnStatement);
    }

    public void AddColumn()
    {
        _cn.Execute(SqlStatements.AddPhoneNumberColumnStatement);
    }

    public bool ContainsPhoneNumberColumn()
    {
        return _cn.QueryFirst<string>(SqlStatements.ColumnNames) == "TRUE";
    }
}