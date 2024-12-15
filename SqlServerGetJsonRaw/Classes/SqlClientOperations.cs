using System.Data;
using Microsoft.Data.SqlClient;
using SqlServerGetJsonRaw.Classes.Configuration;
using SqlServerGetJsonRaw.Models;

namespace SqlServerGetJsonRaw.Classes;

internal class SqlClientOperations
{
    /// <summary>
    /// Retrieves a list of <see cref="PersonSqlClient"/> objects, including their associated addresses, 
    /// filtered by the specified last name.
    /// </summary>
    /// <param name="lastName">The last name used to filter the persons.</param>
    /// <returns>A list of <see cref="PersonSqlClient"/> objects containing person details and their addresses.</returns>
    /// <remarks>
    /// This method establishes a connection to the database, executes a SQL query to fetch person details 
    /// and their associated addresses, and maps the results to a list of <see cref="PersonSqlClient"/> objects.
    /// </remarks>
    public List<PersonSqlClient> GetPerson(string lastName)
    {
        using var cn = new SqlConnection(DataConnections.Instance.MainConnection);
        cn.Open();

        using var cmd = new SqlCommand(SqlStatements.GetPersonAddressesDapperOrSqlClient, cn);

        cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar)).Value = lastName;

        var reader =  cmd.ExecuteReader();
        var people = new List<PersonSqlClient>();

        while (reader.Read())
        {
            var person = people.FirstOrDefault(p => p.Id == reader.GetInt32(0));
            if (person == null)
            {
                person = new PersonSqlClient
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    DateOfBirth = reader.GetDateTime(3),
                    Addresses = []
                };
                people.Add(person);
            }

            person.Addresses.Add(
                new Address(
                    reader.GetString(4), 
                    reader.GetString(5), 
                    reader.GetString(6)));
        }

        return people;
    }

    /// <summary>
    /// Retrieves a list of last names from the database, ordered alphabetically.
    /// </summary>
    /// <returns>A list of strings representing the last names.</returns>
    public List<string> LastNames()
    {
        List<string> lastNames = new();
        const string sql =
            """
            SELECT 
                LastName 
            FROM 
                dbo.Person 
            ORDER BY 
                LastName;
            """;
        using SqlConnection cn = new(DataConnections.Instance.MainConnection);
        using SqlCommand cmd = new() {Connection = cn, CommandText = sql};
        cn.Open();

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
           lastNames.Add(reader.GetString(0)); 
        }

        return lastNames;
    }
}
