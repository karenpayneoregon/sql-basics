# About

```csharp
public static class SqlClientExtensions
{
    /// <summary>
    /// Write SQL to console or delimited text file
    /// </summary>
    /// <param name="cn">Live <see cref="SqlConnection"/> connection </param>
    /// <param name="statement">SQL Select statement</param>
    /// <param name="fileName">If present write to file else write to console</param>
    /// <returns></returns>
    public static async Task WriteTo(this SqlConnection cn, string statement, string fileName = null)
    {
        await using SqlCommand cmd = new() { Connection = cn, CommandText = statement };

        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        int fieldCount = reader.FieldCount;
        object[] resultRow = new object[fieldCount];

        StringBuilder stringBuilder = new();

        while (reader.Read())
        {
            reader.GetSqlValues(resultRow);
            stringBuilder.AppendLine(string.Join(",", resultRow));
        }

        reader.Close();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine(stringBuilder);
        }
        else
        {
            await File.WriteAllTextAsync(fileName, stringBuilder.ToString());
        }
    }

    /// <summary>
    /// Write query results to a file or to the console
    /// </summary>
    /// <param name="cmd">Live <see cref="SqlCommand"/></param>
    /// <param name="fileName">If not null writes query results to a file else display results to the console</param>
    /// <returns></returns>
    public static async Task WriteTo(this SqlCommand cmd, string fileName = null)
    {
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        int fieldCount = reader.FieldCount;
        object[] resultRow = new object[fieldCount];

        StringBuilder stringBuilder = new();

        while (reader.Read())
        {
            reader.GetSqlValues(resultRow);
            stringBuilder.AppendLine(string.Join(",", resultRow));
        }

        reader.Close();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine(stringBuilder);
        }
        else
        {
            await File.WriteAllTextAsync(fileName, stringBuilder.ToString());
        }
    }
}
```