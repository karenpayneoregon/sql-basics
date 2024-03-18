using Microsoft.Data.SqlClient;
using System.Data;
using ConsoleApp1.Models;
using Dapper;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using ConsoleApp1.Handlers;

namespace ConsoleApp1.Classes;
internal class DapperOperation1
{

    private readonly IDbConnection _cn;

    public DapperOperation1()
    {
        _cn = new SqlConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    /// <summary>
    /// Get all records in the Person table synchronously
    /// </summary>
    public List<Person> GetAll()
        => _cn.Query<Person>(SqlStatements.ReadPeople).AsList();

    public async Task<List<Person>> GetAllAsync()
    {
        return (List<Person>)await _cn.QueryAsync<Person>(SqlStatements.ReadPeople);
    }
}
