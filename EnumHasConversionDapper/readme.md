# About

Working with NuGet package [StrEnum.Dapper](https://www.nuget.org/packages/StrEnum/2.0.2?_src=template) which converts string to Enum for [Dapper](https://www.nuget.org/packages/Dapper). 

Note this library stores enum as a string in the database rather than an int. This can be problematic if a wine-type name were to change. For this reason I will follow this up with a int conversion next. 

All code was modeled from the project, in this solution EnumHasConversion using EF Core.


## Before running this project

Create database and populate

1. Connection string is in appsettings.json using `(localdb)\\mssqllocaldb` change if desire.
1. Create `WineDapper` database
1. Run `Scripts\script.sql` in this project to create tables and populate data.

## Intialize StrEnum.Dapper

Initialization
Invoke the `StrEnumDapper.UseStringEnums` method before the first use of Dapper:

```csharp
public class WineOperations
{
    private IDbConnection db = new SqlConnection(ConnectionString());
    public WineOperations()
    {
        StrEnumDapper.UseStringEnums();
    }

    ...
}
```

## Model for Enum

```csharp
public class WineType : StringEnum<WineType>
{
    public static readonly WineType Red = Define("Red");
    public static readonly WineType White = Define("White");
    public static readonly WineType Rose = Define("Rose");
}
```

## Main Model

```csharp
public class Wine
{
    public int WineId { get; set; }
    public string Name { get; set; }
    public WineType WineType { get; set; }
    public override string ToString() => $"{WineType} {Name}";
}
```

## Usage

Class for reading, adding, finding and deleting wines

```csharp
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


```

