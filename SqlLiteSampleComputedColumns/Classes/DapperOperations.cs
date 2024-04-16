using Dapper;
using System.Data;
using System.Data.SQLite;

namespace SqlLiteSampleComputedColumns.Classes;
public class DapperOperations
{
    private static string ConnectionString() => "Data Source=sample1.db";

    /// <summary>
    /// Read from a view
    /// </summary>
    public static List<Person> GetPeopleInView()
    {
        var cn = new SQLiteConnection(ConnectionString());

        return cn.Query<Person>(
                """
                SELECT * FROM GetComputedData
                """)
            .AsList();
    }

    /// <summary>
    /// Read people from a table
    /// </summary>
    public static List<Person> GetPeople()
    {
        var cn = new SQLiteConnection(ConnectionString());

        return cn.Query<Person>(
                """
                SELECT Id, FirstName, LastName, FullName FROM ComputedSample1
                """)
            .AsList();
    }
}