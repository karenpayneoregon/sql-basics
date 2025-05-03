using Dapper;
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
    /// Retrieves a list of people from the "ComputedSample1" table in the SQLite database.
    /// </summary>
    /// <returns>A list of <see cref="Person"/> objects containing Id, FirstName, LastName, and FullName.</returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that selects the columns Id, FirstName, LastName, and FullName
    /// from the "ComputedSample1" table. Ensure the database and table exist before calling this method.
    /// </remarks>
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