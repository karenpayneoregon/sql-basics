# About

Provides starter/basic EF Core code for SQLite database.

:bulb: The benefit here is that the database is copied over each time so that developers can experiment with the code and database without worrying about breaking anything. The database is copied from the `Database` folder to the `bin` folder on build.

The database was ported from SQL Server using the following [tool](https://github.com/karenpayneoregon/sqlserver-to-sqllite).

> **Note**
> There are a few advance features used.

- EF Core Power Tools for [Reverse Engineering](https://github.com/ErikEJ/EFCorePowerTools/wiki/Reverse-Engineering) SQLite database to C# code.
   - Requires Visual Studio extension [VisualStudio.Data.Sqlite](https://marketplace.visualstudio.com/items?itemName=bricelam.VSDataSqlite)


## Connection string


**appsettings.json**

```json
{
  "ConnectionStrings": {
    "MainConnection": "Data Source=Database\\NorthWind2024.db"
  },
  "EntityConfiguration": {
    "CreateNew": true
  },
  "ContextOptions": {
    "UseAuditInterceptor": true,
    "CustomersOptions": {
      "UseQueryFilter": true
    }
  }
}
```

## Resources

- Microsoft (C#) [data documentation hub](https://learn.microsoft.com/en-us/ef/dotnet-data/)
- Microsoft [Entity Framework documentation hub](https://learn.microsoft.com/en-us/ef/)
- XML [documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags) for code