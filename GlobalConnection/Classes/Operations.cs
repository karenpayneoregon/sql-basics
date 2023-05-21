using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using Dapper;

namespace GlobalConnection.Classes;
public class Operations
{
    public static async Task<Dictionary<int, string>> CatDictionary()
    {
        await using SqlConnection cn = new(ConnectionString());
        Dictionary<int, string> dict = cn.Query(SqlStatements.CategoryShort()).ToDictionary(
            row => (int)row.CategoryID,
            row => (string)row.CategoryName);

        return dict;
    }
}
