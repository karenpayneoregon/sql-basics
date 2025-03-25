using ConnectionsLibrary;
using FactoryConnectionSample.Classes;
using Dapper;

namespace FactoryConnectionSample;

internal partial class Program
{
    static void Main(string[] args)
    {
        using var cn = DbConnection.CreateConnection();
        var people = cn.Query<Person>(SqlStatements.Read).ToList();

        foreach (var (index, p) in people.Index())
        {
            AnsiConsole.MarkupLine(index.IsOdd()
                ? $"[cyan]{p.Id,-4}{p.FirstName,-10}{p.LastName,-12}{p.BirthDate,-12:MM/dd/yyyy}{p.Age}[/]"
                : $"[white]{p.Id,-4}{p.FirstName,-10}{p.LastName,-12}{p.BirthDate,-12:MM/dd/yyyy}{p.Age}[/]");
        }

        Console.WriteLine();
        AnsiConsole.MarkupLine($"[{Color.Yellow}]Done[/]");

        Console.ReadLine();

    }

}