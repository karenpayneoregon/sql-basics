using System.Data.SQLite;
using Dapper;
using SqlLiteSample1.Models;

namespace SqlLiteSample1.Classes;
internal class DapperOperations
{

    private static string ConnectionString()
        => "Data Source=sample1.db";

    public static void CreateDatabase()
    {
        SQLiteConnection.CreateFile("sample1.db");
    }

    public static void CreateTableWithTwoRecords()
    {
        using var cn = new SQLiteConnection("Data Source=sample1.db");
        cn.Execute(
            """
                   CREATE TABLE IF NOT EXISTS Person
                   (
                       Id INTEGER PRIMARY KEY AUTOINCREMENT,
                       FirstName TEXT NOT NULL,
                       LastName TEXT NOT NULL,
                       Pin INTEGER NOT NULL
                    )
            """);

        List<Person> people =
        [
            new Person { FirstName = "Henry", LastName = "White", Pin = 98762 },
            new Person { FirstName = "Jim", LastName = "Beam", Pin =  89896 }
        ];

        AddRange(people);
    }

    // bulk insert
    public static void AddRange(List<Person> list)
    {
        var cn = new SQLiteConnection(ConnectionString());
        cn.Execute(
            """
            INSERT INTO Person (FirstName,LastName,Pin)
            VALUES (@FirstName, @LastName, @Pin);
            """, list);
    }

    /// <summary>
    /// Get pin for person by id
    /// </summary>
    /// <param name="id">Valid person primary key</param>
    /// <returns>
    /// Pin value for the person with the given id
    /// </returns>
    /// <remarks>
    /// Anonymous type is used to pass the parameter to the query
    /// </remarks>
    public static int GetPin(int id)
    {
        var cn = new SQLiteConnection(ConnectionString());
        return cn.QueryFirstOrDefault<int>(
            """
            SELECT Pin
            FROM Person
            WHERE Id = @Id
            """,
            new { Id = id });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Valid person primary key</param>
    /// <param name="value">new pin</param>
    /// <remarks>
    /// Anonymous type is used to pass the parameter to the query
    /// </remarks>
    public static void SetPin(int id, int value)
    {
        var cn = new SQLiteConnection(ConnectionString());

        cn.Execute(
            """
            UPDATE Person
            SET Pin = @Pin
            WHERE Id = @Id
            """,
            new { Id = id, Pin = value });

    }

    /// <summary>
    /// Read all people from the database
    /// </summary>
    /// <remarks>
    /// .AsList() is used to ensure that the data is read into a list.
    /// </remarks>
    public static void GetPeople()
    {
        var cn = new SQLiteConnection(ConnectionString());

        var people = cn.Query<Person>(
                """
                SELECT Id,
                       FirstName,
                       LastName,
                       Pin
                FROM Person
                ORDER BY LastName
                """)
            .AsList();

        var table = SpectreConsoleHelpers.CreateTable();

        foreach (var person in people)
        {
            table.AddRow(
                person.Id.ToString(), 
                person.FirstName, 
                person.LastName, 
                person.Pin.ToString());
        }

        AnsiConsole.Write(table);
        
    }
}
