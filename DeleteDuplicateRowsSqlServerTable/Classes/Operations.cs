using System.Data;
using Dapper;
using DeleteDuplicateRowsSqlServerTable.Models;
using kp.Dapper.Handlers;
using Microsoft.Data.SqlClient;

namespace DeleteDuplicateRowsSqlServerTable.Classes;


/// <summary>
/// Provides operations for managing and manipulating data in the 
/// <c>PersonWithDuplicates</c> table within the SQL Server database.
/// </summary>
/// <remarks>
/// This class includes methods for populating the table with predefined data, 
/// removing duplicate rows, resetting the table to its initial state, 
/// and retrieving data. It leverages Dapper for database interactions.
/// </remarks>
internal class Operations
{
    // Usually in appsettings.json
    private readonly IDbConnection _cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Examples;Integrated Security=True;Encrypt=False");

    /// <summary>
    /// Setup for Dapper to work with DateOnly
    /// Create a connection to the database
    /// </summary>
    public Operations()
    {
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    /// <summary>
    /// Read current data
    /// </summary>
    public List<PersonWithDuplicates> GetAll()
        => _cn.Query<PersonWithDuplicates>(
            """
            SELECT Id, FirstName, LastName, BirthDay 
            FROM Examples.dbo.PersonWithDuplicates;
            """).ToList();

    /// <summary>
    /// Removes all existing rows from the <c>PersonWithDuplicates</c> table, resets the identity seed, 
    /// and inserts a predefined set of rows with potential duplicates.
    /// </summary>
    /// <remarks>
    /// This method is useful for resetting the table to a known state with predefined data, 
    /// including duplicate rows for testing or demonstration purposes.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL commands.
    /// </exception>
    public void Populate()
    {
        
        ResetTable();
        
        _cn.Execute(
            """
            INSERT INTO PersonWithDuplicates ([FirstName], [LastName], [BirthDay])
            VALUES
            (N'Bill', N'Smith', N'1976-09-01' ),
            (N'Mary', N'Robinson', N'1945-12-12' ),
            (N'Bill', N'Smith', N'1976-09-01' ),
            (N'Bill', N'Smith', N'1976-09-01' ),
            (N'Nancy', N'Jones', N'2000-02-23' ),
            (N'Nancy', N'Johnson', N'2005-08-12' ),
            (N'Nancy', N'Jones', N'2000-02-23' ),
            (N'Karen', N'Payne', N'1956-09-09' ),
            (N'Kim', N'Adams', N'1989-07-12' ),
            (N'Karen', N'Payne', N'1956-09-09' )
            """);
    }

    /// <summary>
    /// Deletes all rows from the <c>PersonWithDuplicates</c> table and resets the identity seed to zero.
    /// </summary>
    /// <remarks>
    /// This method ensures that the table is emptied and its identity column is reset, 
    /// allowing new rows to start with an identity value of 1.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL commands.
    /// </exception>
    public void ResetTable()
    {
        _cn.Execute($"DELETE FROM dbo.{nameof(PersonWithDuplicates)}");
        _cn.Execute($"DBCC CHECKIDENT ({nameof(PersonWithDuplicates)}, RESEED, 0)");
    }

    /// <summary>
    /// Removes duplicate rows from the <c>PersonWithDuplicates</c> table in the database.
    /// </summary>
    /// <remarks>
    /// This method executes a SQL statement to delete duplicate rows while retaining the row with the minimum <c>Id</c>.
    /// Duplicates are determined based on matching <c>FirstName</c>, <c>LastName</c>, and other relevant columns.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL statement on the database.
    /// </exception>
    /// <example>
    /// <code>
    /// Operations operations = new();
    /// operations.RemoveDuplicates();
    /// </code>
    /// </example>
    public void RemoveDuplicates()
    {
        _cn.Execute(SqlStatements.DeleteStatement);
    }

    public void YourTableDuplicates()
    {

        var list = _cn.Query<ItemContainer>(SqlStatements.CreatePopulateTableGetDuplicates).ToList();
        
        var result = list.GroupBy(x => 
            new { x.Name, x.Email }, (key, group) => new
        {
            key.Name,
            key.Email,
            List = group.ToList()
        });

        Console.WriteLine("Name     Email");
        foreach (var data in result)
        {
            Console.WriteLine($"{data.Name,-9}{data.Email}");
            foreach (var item in data.List)
            {
                Console.WriteLine($"  {item.Id}");
            }
        }

    }

}