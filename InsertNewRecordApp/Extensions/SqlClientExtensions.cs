using Microsoft.Data.SqlClient;

namespace InsertNewRecordApp.Extensions;

public static class SqlClientExtensions
{
    public static DateOnly GetDateOnly(this SqlDataReader reader, int index)
        => reader.GetFieldValue<DateOnly>(index);

}