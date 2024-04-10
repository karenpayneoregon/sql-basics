using Dapper;
using System.Data;
using System.Data.SQLite;

namespace SqlLiteSampleComputedColumns.Classes;
internal class DapperOperations
{
    private static string ConnectionString()
        => "Data Source=sample1.db";
    public static List<Person> GetPeople()
    {
        var cn = new SQLiteConnection(ConnectionString());

        return cn.Query<Person>(
                """
                SELECT Id,
                       FirstName,
                       LastName,
                       FullName
                FROM ComputedSample
                """)
            .AsList();
    }
}

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
}
