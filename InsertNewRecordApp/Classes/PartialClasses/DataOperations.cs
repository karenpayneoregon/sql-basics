using Dapper;
using Microsoft.Data.SqlClient;

// ReSharper disable once CheckNamespace
namespace InsertNewRecordApp.Classes;
public partial class DataOperations
{

    /// <summary>
    /// Dump all records in the Person table and reset identity
    /// </summary>
    public static async Task Reset()
    {
        await using SqlConnection cn = new(ConnectionString());
        await using SqlCommand cmd = new() { Connection = cn, CommandText = "DELETE FROM dbo.Person" };
        await cn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
        cmd.CommandText = "DBCC CHECKIDENT (Person, RESEED, 0)";
        await cmd.ExecuteNonQueryAsync();
    }


    /// <summary>
    /// Test connection with provided connection string
    /// </summary>
    /// <returns></returns>
    static async Task<(bool, SqlException exception)> CanConnect()
    {
        CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(4));

        await using SqlConnection cn = new(ConnectionString());

        try
        {
            await cn.OpenAsync(cancellationTokenSource.Token);
            return (true, null);
        }
        catch (SqlException exception)
        {
            return (false, exception);
        }
    }
}
