using System.Data;
using Dapper;
using InsertNewRecordApp.Models;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
namespace InsertNewRecordApp.Classes;


internal class DataOperations
{
    public static async Task<(bool success, Exception exception)> AddRange(List<Person> list)
    {
        static async Task<(bool, SqlException exception)> CanConnect()
        {
            await using SqlConnection connection = new(ConnectionString());
            try
            {
                await connection.OpenAsync();
                return (true, null);
            }
            catch (SqlException exception)
            {
                return (false,exception);
            }
        }

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

            for (int index = 0; index < list.Count; index++)
            {
                cmd.Parameters["@FirstName"].Value = list[index].FirstName;
                cmd.Parameters["@LastName"].Value = list[index].LastName;
                cmd.Parameters["@BirthDate"].Value = list[index].BirthDate;
                list[index].Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
    /// Get all records for Person table
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
    public static int PeopleCount()
    {
        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.CountOfPeople };

        cn.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public static void Reset()
    {
        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = "DELETE FROM dbo.Person" };
        cn.Open();
        cmd.ExecuteNonQuery();
        cmd.CommandText = "DBCC CHECKIDENT (Person, RESEED, 0)";
        cmd.ExecuteNonQuery();
    }
}


