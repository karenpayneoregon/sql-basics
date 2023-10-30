using Dapper;
using Microsoft.Data.SqlClient;
using Dapper.Transaction;
using InsertNewRecordApp.Handlers;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp.Classes;

public partial class DataOperations
{
    /// <summary>
    /// Add a list of <see cref="Person"/> and provide the primary key
    /// </summary>
    /// <param name="list">list of people</param>
    /// <returns>success and on failure the exception</returns>
    public static async Task<(bool, Exception ex)> AddRangeDapperWithKeys(List<Person> list)
    {

        var (_, sqlException) = await CanConnect();
        if (sqlException is not null)
        {
            return (false, sqlException);
        }

        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());

        try
        {
            await using SqlConnection cn = new(ConnectionString());
            await cn.OpenAsync();

            await using (var transaction = cn.BeginTransaction())
            {
                foreach (var person in list)
                {
                    
                    var primaryKey = await transaction.ExecuteScalarAsync(
                        SqlStatements.InsertPeople, 
                        person);

                    person.Id = (int)primaryKey!;
                }

                await transaction.CommitAsync();

            }

            return (list.All(p => p.Id > 0), null);
        }
        catch (Exception ex)
        {
            return (false, ex);
        }
    }

    /// <summary>
    /// Add a list of <see cref="Person"/> without a primary key
    /// </summary>
    /// <param name="list">list of people</param>
    /// <returns>success and on failure the exception</returns>
    public static async Task<(bool, Exception ex)> AddRangeDapperWithoutKeys(List<Person> list)
    {
        var (_, sqlException) = await CanConnect();
        if (sqlException is not null)
        {
            return (false, sqlException);
        }

        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
        try
        {
            await using SqlConnection cn = new(ConnectionString());
            
            var rowsAffected = await cn.ExecuteAsync(
                SqlStatements.InsertPeople, 
                list);

            return (rowsAffected > 0, null);
        }
        catch (Exception ex)
        {
            return (false, ex);
        }
    }

    /// <summary>
    /// Add a single <see cref="Person"/> with its new primary key
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    public static async Task AddDapper(Person person)
    {
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
        await using SqlConnection cn = new(ConnectionString());

        var primaryKey = await cn.ExecuteScalarAsync(
            SqlStatements.InsertPeople, 
            person);

        person.Id = (int)primaryKey!;
    }


    /// <summary>
    /// Get all records for Person table using Dapper
    /// </summary>
    public static List<Person> GetAll()
    {
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());

        using SqlConnection cn = new(ConnectionString());

        return cn.Query<Person>(SqlStatements.ReadPeople).ToList();

    }

    /// <summary>
    /// Get a <see cref="Person"/> by primary key
    /// </summary>
    /// <param name="id">key</param>
    /// <returns>A Person or null if not found</returns>
    public static async Task<Person> GetDapper(int id)
    {
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());

        await using SqlConnection cn = new(ConnectionString());
       
        return await cn.QuerySingleAsync<Person>(
            SqlStatements.Get, new 
                {Id = id});

    }

    /// <summary>
    /// Get count for Person table
    /// </summary>
    /// <returns>Count of records</returns>
    public static async Task<int> PeopleCountDapper()
    {
        await using SqlConnection cn = new(ConnectionString());
        return await cn.ExecuteScalarAsync<int>(SqlStatements.CountOfPeople);
    }

    /// <summary>
    /// Update a record
    /// </summary>
    /// <param name="person"></param>
    /// <returns>True if successful, false on failure</returns>
    /// <remarks>
    /// Parameters in SQL are the same names as the properties in the Person
    /// model, if they were different than we need to use the parameter name
    /// e.g. if the parameter for Id was Identifier we use Identifier = person.Id
    ///
    /// Also, performing an INSERT works the same as an UPDATE syntax-wise.
    /// 
    /// </remarks>
    public static async Task<(bool, Exception ex)> UpdateDapper(Person person)
    {
        try
        {

            await using SqlConnection cn = new(ConnectionString());

            var affected = await cn.ExecuteAsync(
                SqlStatements.UpdatePerson, new
            {
                person.FirstName,
                person.LastName,
                person.BirthDate,
                person.Id
            });

            return (affected == 1, null);

        }
        catch (Exception ex)
        {
            return (false, ex);
        }
    }

    /// <summary>
    /// Remove <see cref="Person"/> from the database
    /// </summary>
    /// <param name="person">Person with a id</param>
    /// <returns>success</returns>
    public static async Task<bool> RemoveDapper(Person person)
    {
        await using SqlConnection cn = new(ConnectionString());

        var affected = await cn.ExecuteAsync(
            SqlStatements.RemovePerson, 
            new { person.Id }
            );

        return affected == 1;

    }
}
