using Microsoft.Data.SqlClient;
using Dapper;

namespace DateTimeNullDapperApp.Classes;
public class DataOperations
{

    public static async Task<List<DateTimeWithNulls>> ReadDataAsync()
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                                  "Initial Catalog=Karen1;Integrated Security=True;Encrypt=False";

        const string sql = "SELECT Id,BirthDate FROM dbo.DateTimeWithNulls";

        await using var connection = new SqlConnection(connectionString);
        return (await connection.QueryAsync<DateTimeWithNulls>(sql)).ToList();

    }
}

public class DateTimeWithNulls
{
    public int Id { get; set; }
    public DateTime? BirthDate { get; set; }
}