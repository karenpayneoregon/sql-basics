
using Dapper;
using Microsoft.Data.SqlClient;
using SqlServerOutputToOtherTableExample.Classes;
using SqlServerOutputToOtherTableExample.Models;

namespace SqlServerOutputToOtherTableExample;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();
        List<Person> list =
        [
            new() { FirstName = "Mary", LastName = "Adams", Gender = "Female" },
            new() { FirstName = "Bill", LastName = "Jones", Gender = "Male" }
        ];

        Person p = new Person() { FirstName = "Karen", LastName = "Payne", Gender = "Female"};
        
        await using var cn = new SqlConnection(DataConnections.Instance.MainConnection);

        AnsiConsole.MarkupLine("[yellow]Adding a single record[/]");
        var identifier = await cn.QueryFirstAsync<int>(SqlStatements.InsertPerson, p);
        AnsiConsole.MarkupLine($"[cyan]Identifier: {identifier}[/]");

        AnsiConsole.MarkupLine("[yellow]Adding records[/]");
        await cn.ExecuteAsync(SqlStatements.InsertPersonToTransactions, list);
        AnsiConsole.MarkupLine("[green]Records added[/]");
        ExitPrompt();
    }
}