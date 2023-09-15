using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DateLibrary;
internal class Examples
{
    private static string ConnectionString => 
            """
            Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=NorthWind2022;
            Integrated Security=True
            ;Encrypt=False
            """;

    public static async Task<(int, DateTime)> No(string productName)
    {
        await using SqlConnection cn = new(ConnectionString);
        await using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.GetProductByName() };
        cmd.Parameters.AddWithValue("@ProductName", productName);
        await cn.OpenAsync();
        SqlDataReader? reader = await cmd.ExecuteReaderAsync();
        reader.Read();
        return (reader.GetInt32(0), reader.GetDateTime(1));
    }
    [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]

    public static async Task<(int, DateTime)> Yes(string productName)
    {
        await using SqlConnection cn = new(ConnectionString);
        await using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.GetProductByName() };
        cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = productName;
        await cn.OpenAsync();

        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
            reader.Read();
            return (reader.GetInt32(0), reader.GetDateTime(1));
        }
    }
}
