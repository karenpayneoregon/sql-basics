# About

This is a test project to test out .NET Core 8 Dapper, and EF Core 8

Will be moving Dapper code to another formal project.

Experimenting with .NET Core 8. Currently Resharper is causing issues.

- Resharper enabled, caused issues with .NET Core 8
- Resharper disabled, zero issues with .NET Core 8

Sent a bug report to Jetbrains. 

---

## EF Core 8

Appears faster than EF Core 7

---

## Dapper.AOT

See code in `DapperOperations.cs` and `SqlStatement.cs`

https://aot.dapperlib.dev/gettingstarted

Above namespace

```csharp
[module: DapperAot]
[module: DapperAot(false)]
```

Then to check SQL syntax

```csharp
[SqlSyntax(SqlSyntax.SqlServer)]
internal class DapperOperations
```

