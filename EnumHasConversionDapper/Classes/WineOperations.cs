using System.Data;
using Dapper;
using EnumHasConversionDapper.Models;
using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using StrEnum.Dapper;

namespace EnumHasConversionDapper.Classes;
public class WineOperations
{
    private IDbConnection db = new SqlConnection(ConnectionString());
    public WineOperations()
    {
        StrEnumDapper.UseStringEnums();
    }

    /// <summary>
    /// Read all wines ordered by wine name
    /// </summary>
    public List<Wine> AllWines()
    {
        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wines
            ORDER BY Name, WineType
            """;

        return db.Query<Wine>(statement).AsList();
    }

    /// <summary>
    /// Read specific <see cref="WineType"/> wines"/>
    /// </summary>
    /// <param name="wineType">Type of wine</param>
    public List<Wine> SpecificWines(WineType wineType)
    {
        const string statement =
            """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wines
            WHERE WineType = @WineType
            """;

        return db.Query<Wine>(statement, new { WineType = wineType}).AsList();

    }

    /// <summary>
    /// Add a new wine
    /// </summary>
    /// <param name="name">wine name</param>
    /// <param name="wineType">wine type</param>
    public int AddWine(string name, WineType wineType)
    {
        const string statement =
            """
            INSERT INTO dbo.Wines (Name, WineType)
              VALUES (@Name, @WineType);
            SELECT CAST(scope_identity() AS int);
            """;

        return db.QueryFirst<int>(statement, new { Name = name, WineType = wineType });
    
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
             FROM [dbo].[Wines]
            WHERE Name     = @Name
              AND WineType = @WineType;
            """;

        return db.QueryFirstOrDefault<Wine>(statement, new { Name = name, WineType = wineType }) != null;
    }

    /// <summary>
    /// Read wines by <see cref="WineType"/> using WHERE IN
    /// </summary>
    /// <param name="wineTypes">Array of <see cref="wineTypes"/></param>
    public async Task<List<Wine>> WhereIn(string[] wineTypes)
    {
        string WhereInClause =
        """
            SELECT WineId
                ,Name
                ,WineType
            FROM dbo.Wines
            WHERE WineType IN @WineType;
            """;

        IEnumerable<Wine> result = await db.QueryAsync<Wine>(
            WhereInClause, new
            {
                WineType = wineTypes
            });

        return result.ToList();
    }

    /// <summary>
    /// Remove Wine record from table by primary key
    /// </summary>
    /// <param name="wineId">Existing wine record id</param>
    public void RemoveWineById(int wineId)
    {
        const string statement =
            """
            DELETE FROM dbo.Wines 
            WHERE WineId = @WineId;
            """;

        db.Execute(statement, new { WineId = wineId });
    }

}
