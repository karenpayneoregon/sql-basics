using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using SqlServerGetJsonRaw.Classes.Configuration;
using SqlServerGetJsonRaw.Models;
using System.Data.Common;

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
internal class DapperOperations
{
    private IDbConnection _cn = new SqlConnection(DataConnections.Instance.MainConnection);

    /// <summary>
    /// Retrieves a list of person addresses based on the provided last name.
    /// </summary>
    /// <param name="lastName">The last name used to filter the person addresses.</param>
    /// <returns>
    /// A list of <see cref="PersonDapper1"/> objects containing the person's
    /// details and their associated addresses.
    /// </returns>
    private List<PersonDapper1> GetPerson(string lastName)
    {

        var list = new List<PersonDapper1>();
        using var reader = _cn.QueryMultiple(SqlStatements.GetPersonAddressesDapper, new {LastName = lastName});

        while (!reader.IsConsumed)
        {
            var partialResult = reader.Read<PersonDapper1>();
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
    /// <description>A list of <see cref="PersonDapper1"/> objects representing the person's details.</description>
    /// </item>
    /// <item>
    /// <description>A list of <see cref="Address"/> objects representing the associated addresses.</description>
    /// </item>
    /// </list>
    /// </returns>
    public (List<PersonDapper1> person, List<Address> addresses) PersonData1(string lastName)
    {
        List<PersonDapper1> result = GetPerson(lastName);

        var addresses = result.Select(p => 
            new Address(p.Street, p.City, p.Company)).ToList();

        result.RemoveAt(0);

        return (result, addresses);
    }

    /// <summary>
    /// Retrieves a list of persons and their associated addresses from the database 
    /// based on the provided last name.
    /// </summary>
    /// <param name="lastName">
    /// The last name used to filter the persons in the database.
    /// </param>
    /// <returns>
    /// A list of <see cref="PersonDapper2"/> objects, where each object contains 
    /// person details and their associated addresses.
    /// </returns>
    /// <remarks>
    /// This method uses Dapper to execute a SQL query that retrieves person details 
    /// and their associated addresses. The results are mapped into a dictionary to 
    /// ensure that each person is associated with their respective addresses.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue executing the SQL query.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the database connection is not properly initialized.
    /// </exception>
    public List<PersonDapper2> PersonData2(string lastName)
    {

        var dict = new Dictionary<int, PersonDapper2>();

        _cn.Query<PersonDapper2, Address, PersonDapper2>(SqlStatements.GetPersonAddressesDapperOrSqlClient, (person, address) =>
            {
                if (!dict.TryGetValue(person.Id, out var existingPerson))
                {
                    existingPerson = person;
                    existingPerson.Addresses = [];
                    dict[person.Id] = existingPerson;
                }

                if (address != null)
                {
                    existingPerson.Addresses.Add(address);
                }

                return existingPerson;
            },
            new { LastName = lastName },
            splitOn: "Street" // split between Person and Address 
        );

        return dict.Values.ToList();
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

