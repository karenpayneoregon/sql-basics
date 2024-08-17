using Microsoft.Data.SqlClient;

namespace InsertNewRecordApp.Extensions;

public static class SqlClientExtensions
{
    public static async Task<DateOnly> GetDateOnlyAsync(this SqlDataReader reader, int index)
        => await reader.GetFieldValueAsync<DateOnly>(index);
    public static async Task<string> GetStringAsync(this SqlDataReader reader, int index)
        => await reader.GetFieldValueAsync<string>(index);
}