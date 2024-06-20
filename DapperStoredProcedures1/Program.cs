using System.Data;
using Dapper;
using DapperStoredProcedures1.Classes;
using DapperStoredProcedures1.Models;
using Dumpify;
using kp.Dapper.Handlers;
using Microsoft.Data.SqlClient;

namespace DapperStoredProcedures1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();

        // Allows Dapper to handle DateOnly types
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());

        await GetAllEmployees();

        Console.WriteLine();

        await GetEmployeeByGender();

        ExitPrompt();
    }

    private static async Task GetEmployeeByGender()
    {

        AnsiConsole.MarkupLine("[cyan]Female employees[/]");

        await using SqlConnection cn = new(DataConnections.Instance.MainConnection);

        // get employees via a stored procedure
        var employees =
            (
                await cn.QueryAsync<Employee>("usp_GetEmployeeByGender", 
                    param: new { GenderId = Genders.Female })
            )
            .AsList();

        // Nicely display the results from the stored procedure
        employees.Dump();
    }

    private static async Task GetAllEmployees()
    {
        AnsiConsole.MarkupLine("[cyan]All employees[/]");

        await using SqlConnection cn = new(DataConnections.Instance.MainConnection);

        // get employees via a stored procedure
        var employees = 
            (
                await cn.QueryAsync<Employee>("usp_GetAllEmployees")
            )
            .AsList();

        // Nicely display the results from the stored procedure
        employees.Dump();
    }
}


