using Microsoft.Data.SqlClient;
using Dapper;

// ReSharper disable once CheckNamespace
namespace InsertNewRecordApp.Classes;
internal partial class DataOperations
{
    /// <summary>
    /// Dump all records in the Person table and reset identity
    /// </summary>
    public static async Task ResetDapper()
    {
        await using SqlConnection cn = new(ConnectionString());
        await cn.ExecuteAsync("DELETE FROM dbo.Person");
        await cn.ExecuteAsync("DBCC CHECKIDENT (Person, RESEED, 0)");
    }
}
