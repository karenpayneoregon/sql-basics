using Microsoft.Data.SqlClient;
using Dapper;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
namespace DapperSamples.Classes;
internal class DataOperations
{
    public static async Task<List<string>> ColumnNames(string tableName)
    {
        await using SqlConnection cn = new(ConnectionString());
        return (List<string>)await cn.QueryAsync<string>(
            SqlStatements.GetColumnNamesStatement, new
            {
                TableName = tableName
            });
    }
    public static async Task<List<ColumnResult>> GetColumnLengths(string statement)
    {
        await using SqlConnection cn = new(ConnectionString());
        var results = await cn.QueryAsync<ColumnResult>(statement);
        return results.ToList();
    }

    public static async Task<List<Columns>> GetColumnLengths1(string tableName)
    {
        await using SqlConnection cn = new(ConnectionString());
        return cn.Query<Columns>(SqlStatements.MaxForColumn(), new
        {
            TableName = tableName,
            SQL = ""
        }).AsList();
    }


}
