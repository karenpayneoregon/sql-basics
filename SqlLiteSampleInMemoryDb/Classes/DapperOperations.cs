using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;

using SqlLiteSampleInMemoryDb.Models;

namespace SqlLiteSampleInMemoryDb.Classes;

public class DapperOperations
{
    public static void Execute()
    {

        const string connectionString = "data source=:memory:;cache=shared;";

        using var cn = new SqliteConnection(connectionString);
        cn.Open();
        CreateTable(cn);

        var people = new List<Person>
        {
            new() { FirstName = "John", LastName = "Doe", Pin = 1234 },
            new() { FirstName = "Mary", LastName = "Adams", Pin = 5555 },
            new() { FirstName = "Karen", LastName = "Payne", Pin = 7334 },
            new() { FirstName = "Jane", LastName = "Doe", Pin = 5678 }
        };

        AddRecords(cn, people);
        UpdateRecord(cn, "Anne", 2);
        ViewRecords(cn);

    }

    private static void CreateTable(IDbConnection cn)
    {
        const string createTableStatement =
            """
            CREATE TABLE Person (
                Id INTEGER PRIMARY KEY,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                FullName TEXT GENERATED ALWAYS AS (FirstName || ' ' || LastName) VIRTUAL,
                Pin INTEGER
            )
            """;


        cn.Execute(createTableStatement);
    }


    private static void UpdateRecord(IDbConnection cn, string firstName, int id)
    {
        cn.Execute(
            """
            UPDATE Person
            SET FirstName = @FirstName
            WHERE Id = @Id
            """,
            new { FirstName = firstName, Id = id });
    }

    private static void ViewRecords(IDbConnection cn)
    {
        var peopleList = cn.Query<Person>(
                """
                SELECT Id, FirstName, LastName, FullName, Pin FROM Person
                """)
            .AsList();

        var dump = ObjectDumper.Dump(peopleList);

        AnsiConsole.MarkupLine(dump.Replace("{Person}", "[cyan]{Person}[/]"));
    }

    private static void AddRecords(IDbConnection cn, List<Person> people)
    {
        const string insertStatement =
            """
            INSERT INTO Person (FirstName,LastName,Pin)
            VALUES (@FirstName,@LastName,@Pin)
            """;
        cn.Execute(insertStatement, people);

    }
}