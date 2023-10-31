using Microsoft.Data.SqlClient;
using Dapper;
using InsertNewRecordApp.Models;
using InsertNewRecordApp.Handlers;

// ReSharper disable once CheckNamespace
namespace InsertNewRecordApp.Classes;
public partial class DataOperations
{
    /// <summary>
    /// Dump all records in the Person table and reset identity
    /// </summary>
    public static async Task ResetDapper()
    {
        await using SqlConnection cn = new(ConnectionString());

        await cn.ExecuteAsync("DELETE FROM dbo.Person");

        await cn.ExecuteAsync("DBCC CHECKIDENT (Person, RESEED, 0)");

    }

    public static async Task SetupWithJson(List<Person> list)
    {
        await ResetDapper();
        await AddRangeDapperWithKeys(list);
    }
    
    /// <summary>
    /// Get people with birth dates between two years
    /// </summary>
    /// <param name="startYear">lower year</param>
    /// <param name="endYear">upper year</param>
    /// <returns>list of person</returns>
    public static List<Person> GetBetweenDates(int startYear, int endYear)
    {

        /*
         * Setup Dapper to understand DateOnly
         */
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());

        using SqlConnection cn = new(ConnectionString());


        var parameters = new
        {
            StartYear = startYear, 
            EndYear = endYear
        };

        /*
         * Dapper
         *  - Query Executes a query, returning the data typed as in this case type Person
         */
        return cn.Query<Person>(SqlStatements.BirthDateBetweenYears, parameters).ToList();

    }
}
