using Dapper;
using Microsoft.Data.SqlClient;
using Dapper.Transaction;
using InsertNewRecordApp.Handlers;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp.Classes;
internal partial class DataOperations
{
    public static async Task<(bool, Exception ex)> AddRangeDapperWithKeys(List<Person> list)
    {
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());

        try
        {
            await using SqlConnection cn = new(ConnectionString());
            await cn.OpenAsync();
            await using (var transaction = cn.BeginTransaction())
            {
                foreach (var person in list)
                {
                    var primaryKey = await transaction.ExecuteScalarAsync(SqlStatements.InsertPeople, person);
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

    public static async Task<(bool, Exception ex)> AddRangeDapperWithoutKeys(List<Person> list)
    {
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
        try
        {
            await using SqlConnection cn = new(ConnectionString());
            var rowsAffected = await cn.ExecuteAsync(SqlStatements.InsertPeople, list);
            return (rowsAffected > 0, null);
        }
        catch (Exception ex)
        {
            return (false, ex);
        }
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
    /// </remarks>
    public static async Task<(bool, Exception ex)> Update(Person person)
    {
        try
        {

            await using SqlConnection cn = new(ConnectionString());

            var affected = await cn.ExecuteAsync(SqlStatements.UpdatePerson, new
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
}
