using System.Data;
using Dapper;
using InsertNewRecordApp.Models;
using Microsoft.Data.SqlClient;

namespace InsertNewRecordApp.Classes;


internal partial class DataOperations
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
        catch (Exception ex)
        {
            transaction.Rollback();
            return (false, ex);
        }
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


