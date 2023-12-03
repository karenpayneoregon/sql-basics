using System.Text;
using Microsoft.Data.SqlClient;

namespace GetValuesApp.Classes;

public static class SqlClientExtensions
{
    /// <summary>
    /// Write query results to a file or to the console
    /// </summary>
    /// <param name="cmd">Live <see cref="SqlCommand"/></param>
    /// <param name="fileName">If not null writes query results to a file else
    /// display results to the console</param>
    /// <returns></returns>
    public static async Task Write(this SqlCommand cmd, string fileName = null)
    {
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        int fieldCount = reader.FieldCount;
        object[] resultRow = new object[fieldCount];

        StringBuilder stringBuilder = new();

        while (reader.Read())
        {
            reader.GetSqlValues(resultRow);
            //reader.GetValues(resultRow);
            stringBuilder.AppendLine(string.Join(",", resultRow));
        }

        // important else if the command has another reader an exception is thrown
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