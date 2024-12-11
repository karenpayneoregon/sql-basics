using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using SqlServerGetJsonRaw.Classes.Configuration;
using SqlServerGetJsonRaw.Models;

namespace SqlServerGetJsonRaw.Classes;
internal class DataOperations
{
    private IDbConnection _cn = new SqlConnection(DataConnections.Instance.MainConnection);

    /// <summary>
    /// Retrieves a list of person addresses based on the provided last name.
    /// </summary>
    /// <param name="lastName">The last name used to filter the person addresses.</param>
    /// <returns>
    /// A list of <see cref="PersonAddress"/> objects containing the person's
    /// details and their associated addresses.
    /// </returns>
    public List<PersonAddress> GetPerson(string lastName)
    {

        var list = new List<PersonAddress>();
        using var multi = _cn.QueryMultiple(SqlStatements.GetPersonAddresses, new {LastName = lastName});

        while (!multi.IsConsumed)
        {
            var partialResult = multi.Read<PersonAddress>();
            list.AddRange(partialResult);
        }

        return list;
    }

    public List<string> LastNames()
    {
        const string sql = "SELECT LastName FROM dbo.Person ORDER BY LastName;";
        return _cn.Query<string>(sql).ToList();
    }
}

