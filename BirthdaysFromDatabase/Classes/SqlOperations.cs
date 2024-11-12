using Microsoft.Data.SqlClient;
using System.Data;
using BirthdaysFromDatabase.Classes.Configuration;
using BirthdaysFromDatabase.Models;
using Dapper;

namespace BirthdaysFromDatabase.Classes;
internal class SqlOperations
{
    private IDbConnection _cn = new SqlConnection(DataConnections.Instance.MainConnection);

    /// <summary>
    /// Retrieves a list of persons from the database.
    /// </summary>
    /// <returns>The list of persons.</returns>
    public List<Person> List() =>
        _cn.Query<Person>(
            """
            SELECT Id,
                 FirstName,
                 LastName,
                 BirthDate,
                 (CAST(FORMAT(GETDATE(), 'yyyyMMdd') AS INTEGER) - CAST(FORMAT(BirthDate, 'yyyyMMdd') AS INTEGER)) / 10000 as Age
            FROM dbo.BirthDays;
            """).ToList();
}