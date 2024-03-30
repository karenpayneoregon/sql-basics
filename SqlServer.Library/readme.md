# About

Provides classes to get all database name, tables and column details for a SQL-Server instance.

All the code in this class project was first written in the project DapperGetDatabaseAndTableNamesApp. Once tested code was moved to this class project along with minor modifications which were done using Reshaper e.g. moving namespaces.

## Main purpose

Although as mentioned above what this library is capable of, the main thing is to promote.

- Developers to write code for usability
- To use classes rather than write all code in one method which many novice developers do
- Learn how to use [Dapper](https://www.nuget.org/packages/Dapper/2.1.35?_src=template) rather than conventional connection and command objects. What about EF Core? EF Core could be used but using Dapper leans itself to these task better than EF Core.


## SQL Statements

All SQL statements are in static strings in the class SqlStatements. If instead the statements were placed into stored procedures it would be harder to visualize them.

Also, this means each server would need the stored procedures created under master.

### Dapper/Stored procedures

Yes Dapper does stored procedures. Here is a basic example.

```csharp
var storedProcedureName = "sp_GetDatabaseDetails";
var results = cn.Query(storedProcedureName, values, commandType: CommandType.StoredProcedure).AsList();
```

