using System.Data;
using System.Data.SqlTypes;
using System.Numerics;
using System.Text;
using Microsoft.Data.SqlClient;

namespace GetValuesApp.Classes;
internal class SqlOperations
{
    private static string ConnectionString =>
        """
        Data Source=.\SQLEXPRESS;Initial Catalog=NorthWind2022;
        Integrated Security=True;
        Encrypt=False
        """;
    public static void GetCustomers()
    {
        using SqlConnection cn = new(ConnectionString);
        using SqlCommand cmd = new() { Connection = cn, CommandText = SqlStatements.ReadCustomers() };

        cn.Open();
        var reader = cmd.ExecuteReader();
        int fieldCount = reader.FieldCount;
        object[] resultRow = new object[fieldCount];
        int customerId = 0;
        while (reader.Read())
        {
            reader.GetSqlValues(resultRow);
            customerId = ((SqlInt32)resultRow[0]).Value;
            AnsiConsole.MarkupLine(customerId.IsOdd()
                ? $"[white]{customerId,-4}[/][white on red]{string.Join(",", resultRow[1..fieldCount])}[/]"
                : $"[white]{customerId,-4}[/]{string.Join(",", resultRow[1..fieldCount])}");
        }
    }

    //public static async Task WriteToFileAndConsoleForConnection()
    //{
    //    await using SqlConnection cn = new(ConnectionString);
    //    await cn.OpenAsync();
    //    //await cn.WriteToFile(SqlStatements.ReadCustomers(), "Customers.txt");
    //    await cn.WriteTo(SqlStatements.ReadCustomers());
    //    //await cn.WriteToConsole(SqlStatements.ReadCustomers());
    //}

    public static async Task WriteToFileAndConsoleForCommand(int contactTypeIdentifier)
    {
        await using SqlConnection cn = new(ConnectionString);
        await using SqlCommand cmd = new()
        {
            Connection = cn, 
            CommandText = SqlStatements.ReadCustomersOwners()
        };

        cmd.Parameters.Add("@ContactTypeIdentifier", SqlDbType.Int).Value = contactTypeIdentifier;

        await cn.OpenAsync();
        await cmd.Write();
    }


}


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



public static class GenericExtensions
{
    public static bool IsOdd<T>(this T sender) where T : INumber<T>
        => T.IsOddInteger(sender);
}
