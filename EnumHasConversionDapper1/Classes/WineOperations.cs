using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using EnumHasConversionDapper1.Models;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace EnumHasConversionDapper1.Classes;
internal class WineOperations
{
    private IDbConnection db = new SqlConnection(ConnectionString());

    /// <summary>
    /// Read all wines
    /// </summary>
    public List<Wine> AllWines()
    {
        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wine
            """;

        return db.Query<Wine>(statement).AsList();
    }
    /// <summary>
    /// Read specific <see cref="WineType"/> wines/>
    /// </summary>
    /// <param name="wineType">Type of wine</param>
    public List<Wine> SpecificWines(WineType wineType)
    {
        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM WinesDapper.dbo.Wine
            WHERE WineType = @WineType
            """;

        return db.Query<Wine>(statement, new { WineType = wineType }).AsList();

    }

    /// <summary>
    /// Check if a wine/wine-type combination exists
    /// </summary>
    /// <param name="name">wine name</param>
    /// <param name="wineType">wine type</param>
    /// <returns></returns>
    public bool WineExist(string name, WineType wineType)
    {
        const string statement =
            """
            SELECT [WineId],
                  [Name],
                  [WineType]
             FROM [dbo].[Wine]
            WHERE Name     = @Name
              AND WineType = @WineType;
            """;

        return db.QueryFirstOrDefault<Wine>(statement, new { Name = name, WineType = wineType }) != null;
    }

    /// <summary>
    /// Read wines by <see cref="WineType"/> using WHERE IN
    /// </summary>
    /// <param name="wineTypes">Array of <see cref="wineTypes"/></param>
    public async Task<List<Wine>> WhereIn(WineType[] wineTypes)
    {
        string WhereInClause =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wine
            WHERE WineType IN @WineType;
            """;
        var values = wineTypes.Select(x => x.IntValue()).ToArray();
        IEnumerable<Wine> result = await db.QueryAsync<Wine>(
            WhereInClause, new
            {
                WineType = values
            });

        return result.ToList();
    }


    #region Needs to be tested - copied in from other project

    /// <summary>
    /// Add a new wine
    /// </summary>
    /// <param name="name">wine name</param>
    /// <param name="wineType">wine type</param>
    public int AddWine(string name, WineType wineType)
    {
        const string statement =
            """
            INSERT INTO dbo.Wine (Name, WineType)
              VALUES (@Name, @WineType);
            SELECT CAST(scope_identity() AS int);
            """;

        return db.QueryFirst<int>(statement, new { Name = name, WineType = wineType });

    }

    public void RemoveWineById(int wineId)
    {
        const string statement =
            """
            DELETE FROM dbo.Wine
            WHERE WineId = @WineId;
            """;

        db.Execute(statement, new { WineId = wineId });
    }

    #endregion
}
