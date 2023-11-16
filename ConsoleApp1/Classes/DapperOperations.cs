using Microsoft.Data.SqlClient;
using System.Data;
using ConsoleApp1.Handlers;
using ConsoleApp1.Models;
using Dapper;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
#pragma warning disable DAP018
#pragma warning disable DAP018
#pragma warning disable CS8603 // Possible null reference return.

[module: DapperAot]
//[module: DapperAot(false)]

namespace ConsoleApp1.Classes;

[SqlSyntax(SqlSyntax.SqlServer)]

internal class DapperOperations
{

    private readonly IDbConnection _cn;

    public DapperOperations()
    {
        _cn = new SqlConnection(ConnectionString());
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    /// <summary>
    /// Get all records in the Person table synchronously
    /// </summary>
    public List<Person> GetAll()
        => _cn.Query<Person>(SqlStatements.ReadPeople).AsList();

    ///// <summary>
    ///// To test Dapper AOT SQL syntax
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public Person GetPerson(int id) => _cn.QueryFirstOrDefault<Person>(
    //    "select * from dbo.Person where Id=@id",
    //    new { Id = id });


    /// <summary>
    /// DAP214: Variable @Id is not declared and no corresponding parameter exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Person GetPerson1(int id)
    {
        const string statement =
            """
                SELECT Id,
                       FirstName,
                       LastName,
                       BirthDate
                FROM dbo.Person
                WHERE Id = @Id
                """;
        return _cn.QueryFirstOrDefault<Person>(
            statement,
            new { Id = id });
    }

    /// <summary>
    /// error DAP206: Error 46010: Incorrect syntax near @ByteArray.
    /// warning DAP236: Parameter 'ByteArray' is not used, but will be included
    /// </summary>
    /// <param name="imageBytes"></param>
    public static int InsertImage(byte[] imageBytes)
    {
        const string statement =
            """
            INSERT INTO dbo.Pictures (Photo) 
            OUTPUT Inserted.Id VALUE (@ByteArray)
            """;
        using var cn = new SqlConnection(ConnectionString());
        var parameters = new { ByteArray = imageBytes };
        return cn.ExecuteScalar<int>(statement, parameters);
    }

}




















