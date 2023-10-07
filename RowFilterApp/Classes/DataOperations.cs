using Microsoft.Data.SqlClient;
using System.Data;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace RowFilterApp.Classes;
internal class DataOperations
{
    public static async Task<DataTable> Filtered()
    {
        DataTable dt = new DataTable();

        await using SqlConnection cn = new(ConnectionString());

        await using SqlCommand cmd = new()
        {
            Connection = cn,
            CommandText = SqlStatements.CompanyNameContains
        };

        await cn.OpenAsync();
        dt.Load(await cmd.ExecuteReaderAsync());

        return dt;
    }
}
