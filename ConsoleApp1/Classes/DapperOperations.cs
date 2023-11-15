using Microsoft.Data.SqlClient;
using System.Data;
using ConsoleApp1.Handlers;
using ConsoleApp1.Models;
using Dapper;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ConsoleApp1.Classes;
internal class DapperOperations
{
    private IDbConnection cn;

    public DapperOperations()
    {
        cn = new SqlConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
    }
    /// <summary>
    /// Get all records in the Person table synchronously
    /// </summary>
    public List<Person> GetAll()
        => cn.Query<Person>(SqlStatements.ReadPeople).ToList();
}
