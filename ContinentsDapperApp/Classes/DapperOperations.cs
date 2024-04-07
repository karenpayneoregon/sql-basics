using Microsoft.Data.SqlClient;
using System.Data;
using ContinentsDapperApp.Models;
using Dapper;

namespace ContinentsDapperApp.Classes;
internal class DapperOperations // yes we can do stored procedures 😊 and code is formatted for a Twitter post
{
    private IDbConnection db = new SqlConnection(ConnectionString());
    public List<Continent> Continents()
    {
        const string statement =
            """
                SELECT -1 AS Id,
                       'Select' AS ContinentName
                UNION ALL
                SELECT Id
                      ,ContinentName
                  FROM GlobalData.dbo.Continents
                """;

        return db.Query<Continent>(statement).AsList();
    }

    public List<Country> Countries(int id)
    {
        const string statement =
            """
                SELECT -1 AS Id,
                       -1 AS ContinentId,
                       'Select' AS CountryName,
                       '' AS CapitalName
                UNION ALL
                SELECT Id,
                       ContinentId,
                       CountryName,
                       CapitalName
                FROM GlobalData.dbo.Countries 
                WHERE ContinentId = @ContinentId
                """;

        return db.Query<Country>(statement, new { ContinentId = id }).AsList();
    }

    public CountryInformation? CountryInformation(int id)
    {
        const string statement =
            """
                SELECT Id
                      ,CountryId
                      ,Fact
                  FROM GlobalData.dbo.CountryInformation
                WHERE CountryId = @CountryId
                """;

        return db.QueryFirstOrDefault<CountryInformation>(statement, new { CountryId = id });
    }

}
