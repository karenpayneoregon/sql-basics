using Dapper;
using System.Data;
using System.Data.SQLite;

namespace SqlLiteSampleComputedColumns.Classes;
public class DapperOperations
{
    private static string ConnectionString() => "Data Source=sample1.db";
    public static List<Person> GetPeopleInView()
    {
        var cn = new SQLiteConnection(ConnectionString());

        return cn.Query<Person>(
                """
                SELECT * FROM GetComputedData
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
