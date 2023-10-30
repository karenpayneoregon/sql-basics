using System.Data;
using InsertNewRecordApp.Extensions;
using InsertNewRecordApp.Models;
using Microsoft.Data.SqlClient;

namespace InsertNewRecordApp.Classes;

/// <summary>
///  - Connection string resides in appsettings.json retrieved using ConfigurationLibrary NuGet package
///  - All SQL statements reside in the class SqlStatements.
///  - SQL statements were written in SSMS first to ensure they work as expected
/// </summary>
/// <remarks>
///  - All code in this class are mirror images of Dapper code samples
/// </remarks>
public partial class DataOperations
{

    /// <summary>
    /// Given a list of <see cref="Person"/> add them to the table with a transaction
    /// </summary>
    /// <param name="list">Properly populated list from Bogus NuGet package</param>
    /// <returns></returns>
    public static async Task<(bool success, Exception exception)> AddRange(List<Person> list)
    {
        var ( _, sqlException) = await CanConnect();
        if (sqlException is not null)
        {
            return (false, sqlException);
        }

        await using SqlConnection cn = new(ConnectionString());

        await cn.OpenAsync();

        await using SqlTransaction transaction = cn.BeginTransaction();

        try
        {
            await using SqlCommand cmd = new()
            {
                Connection = cn,
                CommandText = SqlStatements.InsertPeople,
                Transaction = transaction
            };

            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BirthDate", SqlDbType.Date);

            foreach (var person in list)
            {
                cmd.Parameters["@FirstName"].Value = person.FirstName;
                cmd.Parameters["@LastName"].Value = person.LastName;
                cmd.Parameters["@BirthDate"].Value = person.BirthDate;
                person.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }

            await transaction.CommitAsync();

            return (true, null);

        }
        catch (Exception localException)
        {
            transaction.Rollback();
            return (false, localException);
        }
    }

    /// <summary>
    /// Get a person by primary key
    /// </summary>
    public static async Task<Person> Get(int id)
    {
        await using SqlConnection cn = new(ConnectionString());
        await using SqlCommand cmd = new()
        {
            Connection = cn,
            CommandText = SqlStatements.Get
        };

        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        await cn.OpenAsync();
        var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();

        Person person = new Person
        {
            Id = id,
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            BirthDate = reader.GetDateOnly(3)
        };

        return person;
    }

    /// <summary>
    /// Add a new person to the database table
    /// </summary>
    public static async Task Add(Person person)
    {
        await using SqlConnection cn = new(ConnectionString());
        await using SqlCommand cmd = new()
        {
            Connection = cn,
            CommandText = SqlStatements.InsertPeople
        };

        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = person.Id;
        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = person.FirstName;
        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = person.LastName;
        cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = person.BirthDate;

        await cn.OpenAsync();

        person.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        
    }

    /// <summary>
    /// Update an existing record by primary key
    /// </summary>
    public static async Task<(bool, Exception ex)> Update(Person person)
    {
        try
        {

            await using SqlConnection cn = new(ConnectionString());
            await using SqlCommand cmd = new()
            {
                Connection = cn, 
                CommandText = SqlStatements.UpdatePerson
            };

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = person.Id;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = person.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = person.LastName;
            cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = person.BirthDate;

            await cn.OpenAsync();
            var affected = await cmd.ExecuteNonQueryAsync();

            return (affected == 1, null);

        }
        catch (Exception localException)
        {
            return (false, localException);
        }
    }

    /// <summary>
    /// Remove a record by primary key
    /// </summary>
    public static async Task<bool> Remove(Person person)
    {
        await using SqlConnection cn = new(ConnectionString());
        await using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.RemovePerson };

        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = person.Id;

        await cn.OpenAsync();

        var affected = await cmd.ExecuteNonQueryAsync();

        return affected == 1;

    }
    /// <summary>
    /// Get count for Person table
    /// </summary>
    /// <returns>Count of records</returns>
    public static async Task<int> PeopleCount()
    {
        await using SqlConnection cn = new(ConnectionString());
        await using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.CountOfPeople };

        cn.Open();

        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

}
