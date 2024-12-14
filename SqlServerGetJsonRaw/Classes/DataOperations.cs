using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using SqlServerGetJsonRaw.Classes.Configuration;
using SqlServerGetJsonRaw.Models;

namespace SqlServerGetJsonRaw.Classes;

/// <summary>
/// Provides data access operations for retrieving person and address information 
/// from a SQL Server database. This class utilizes Dapper for database interactions.
/// </summary>
/// <remarks>
/// This class is designed to handle operations such as retrieving person details, 
/// associated addresses, and a list of last names from the database. It serves as a 
/// data layer for the application.
///
/// Yes we could use stored procedures, but for the sake of simplicity and readability SQL statements are used.
/// </remarks>
internal class DataOperations
{
    private IDbConnection _cn = new SqlConnection(DataConnections.Instance.MainConnection);

    /// <summary>
    /// Retrieves a list of person addresses based on the provided last name.
    /// </summary>
    /// <param name="lastName">The last name used to filter the person addresses.</param>
    /// <returns>
    /// A list of <see cref="Person"/> objects containing the person's
    /// details and their associated addresses.
    /// </returns>
    private List<Person> GetPerson(string lastName)
    {

        var list = new List<Person>();
        using var reader = _cn.QueryMultiple(SqlStatements.GetPersonAddresses, new {LastName = lastName});

        while (!reader.IsConsumed)
        {
            var partialResult = reader.Read<Person>();
            list.AddRange(partialResult);
        }

        return list;
    }

    /// <summary>
    /// Retrieves person data and their associated addresses based on the provided last name.
    /// </summary>
    /// <param name="lastName">The last name used to filter the person data and addresses.</param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item>
    /// <description>A list of <see cref="Person"/> objects representing the person's details.</description>
    /// </item>
    /// <item>
    /// <description>A list of <see cref="Address"/> objects representing the associated addresses.</description>
    /// </item>
    /// </list>
    /// </returns>
    public (List<Person> person, List<Address> addresses) PersonData(string lastName)
    {
        List<Person> result = GetPerson(lastName);
        var addresses = result.Select(p => new Address(p.Street, p.City, p.Company)).ToList();
        result.RemoveAt(0);

        return (result, addresses);
    }

    /// <summary>
    /// Retrieves a list of last names from the database, ordered alphabetically.
    /// </summary>
    /// <returns>A list of strings representing the last names.</returns>
    public List<string> LastNames()
    {
        const string sql = 
            """
            SELECT 
                LastName 
            FROM 
                dbo.Person 
            ORDER BY 
                LastName;
            """;
        return _cn.Query<string>(sql).ToList();
    }
}

