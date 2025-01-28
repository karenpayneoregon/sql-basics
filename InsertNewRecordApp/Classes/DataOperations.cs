using System.Data;
using Dapper;
using InsertNewRecordApp.Extensions;
using InsertNewRecordApp.Models;
using Microsoft.Data.SqlClient;
using Serilog;

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
        var (_, sqlException) = await CanConnect();
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
            CommandText = """
                          SELECT Id,
                                 FirstName,
                                 LastName,
                                 BirthDate
                          FROM dbo.Person
                          WHERE Id = @Id;
                          """
        };

        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        await cn.OpenAsync();
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();

        Person person = new()
        {
            Id = id,
            FirstName = await reader.GetStringAsync(1),
            LastName = await reader.GetStringAsync(2),
            BirthDate = await reader.GetDateOnlyAsync(3)
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

/// <summary>
/// https://learn.microsoft.com/en-us/answers/questions/2150026/sqldatareader-error-invalid-attempt-to-read-when-n
/// </summary>
public class Demo
{
    /// <summary>
    /// Generally read from appsettings.json
    /// </summary>
    private static readonly string _connectionString =
        """
        Data Source=.\SQLEXPRESS;
        Initial Catalog=NorthWind2024;
        Integrated Security=True;Encrypt=False
        """;

    public static async Task<int> GetCountConventional()
    {
        try
        {
            await using SqlConnection cn = new(_connectionString);
            await using SqlCommand cmd = new()
            {
                Connection = cn,
                CommandText =
                    """
                    SELECT Max(Len(CompanyName))
                    FROM NorthWind2024.dbo.Customers
                    """
            };

            await cn.OpenAsync();

            var result = await cmd.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : 0;
        }
        catch (Exception ex)
        {
            // uses SeriLog
            Log.Error(ex, "Error occurred while getting the count.");
            return -1;
        }
    }
    public static async Task<int> GetCountDapper()
    {
        try
        {
            const string query =
                """
            SELECT Max(Len(CompanyName))
            FROM NorthWind2024.dbo.Customers
            """;

            await using SqlConnection cn = new(_connectionString);

            var result = await cn.ExecuteScalarAsync<int?>(query);

            return result ?? 0;
        }
        catch (Exception ex)
        {
            // uses SeriLog
            Log.Error(ex, "Error occurred while getting the count.");
            return -1;
        }
    }
}
