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

        await using SqlConnection cn = new(DataConnections.Instance.MainConnection);

        // get employees via a stored procedure
        var employees = 
            (
                await cn.QueryAsync<Employee>("usp_GetAllEmployees", commandType: CommandType.StoredProcedure)
            )
            .AsList();

        // Nicely display the results from the stored procedure
        Console.WriteLine(employees.Dump());

        ExitPrompt();
    }
}