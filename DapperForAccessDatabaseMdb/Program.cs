using System.Data;
using System.Data.OleDb;
using Dapper;
using DapperForAccessDatabaseMdb.Classes;
#pragma warning disable CA1416

namespace DapperForAccessDatabaseMdb;

internal partial class Program
{
    private static IDbConnection _cn;

    private static void Main()
    {
        _cn = new OleDbConnection(ConnectionString());

        Person person = new()
        {
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new(1980, 1, 1),
            Active = true,
            SomeNumber = 123
        };

        Add(person);

        var people = GetAll();

        AnsiConsole.MarkupLine(ObjectDumper.Dump(people));
        Console.ReadLine();
    }

    public static void Add(Person person)
    {
        const string statement =
            """
            INSERT INTO Person
            (
                FirstName,LastName,BirthDate,Active,SomeNumber
            )
            VALUES
            (
                @FirstName,@LastName,@BirthDate,@Active,@SomeNumber
            )
            """;
        _cn.Execute(statement, new
        {
            person.FirstName, 
            person.LastName, 
            person.BirthDate, 
            person.Active,
            person.SomeNumber
        });
    }

    public static List<Person> GetAll()
        => _cn.Query<Person>("SELECT * FROM Person").ToList();
}