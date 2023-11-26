## Introduction

When a developer begins to interact with data as a novice or student will look to storing data in a text file which has many drawbacks, then there are those who will use a database but unsure which of the many paths to take then there are those who want to experiment with different paths.

Here there are three basic paths, working with json for a crude way to store data to using SQL-Server database where there are two paths, the first using [Dapper](https://www.learndapper.com/) and the second [EF Core](https://learn.microsoft.com/en-us/ef/core/).

Incentive for what follows is to present options for working with data at a barebones level with focus on using databases.

There is one table with three string columns and an int auto-incrementing primary key.

When working with Dapper and EF Core there is no need to be concerned for the most part with data types as both currently support common column types and .NET types, for instance EF Core 8 supports both DateOnly and TimeOnly while older versions need assistance.

For instance, before EF Core 8 the following was needed for DateOnly and TimeOnly.

```csharp
internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter()
        : base(d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d)) { }
}

public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter() : base(timeOnly => 
            timeOnly.ToTimeSpan(), 
        timeSpan => TimeOnly.FromTimeSpan(timeSpan)) { }
}
```

Then tell EF Core about the conversion.

```csharp
protected override void ConfigureConventions(ModelConfigurationBuilder builder)
{

    builder.Properties<DateOnly>()
        .HaveConversion<DateOnlyConverter>()
        .HaveColumnType("date");

    builder.Properties<TimeOnly>()
        .HaveConversion<TimeOnlyConverter>()
        .HaveColumnType("time");

    base.ConfigureConventions(builder);

}
```

Dapper at this time does not support DateOnly, so we must tell Dapper how to handle DateOnly.

```csharp
public class DapperSqlDateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly date)
        => parameter.Value = date.ToDateTime(new TimeOnly(0, 0));

    public override DateOnly Parse(object value)
        => DateOnly.FromDateTime((DateTime)value);
}
public class SqlTimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly time)
    {
        parameter.Value = time.ToString();
    }

    public override TimeOnly Parse(object value) => TimeOnly.FromTimeSpan((TimeSpan)value);
}
```

Then before using Dapper call the handler above.

```csharp
private IDbConnection cn;
public Repository()
{
    cn = new SqlConnection(ConnectionString());
    SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
    SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());
}
```

So for the average developer they know how to work with different types thus it does not matter if in the code samples for this article are just strings.

## Using JSON

Json is not a good choice for data operations period. There is no security, all data is exposed as plain text. For CRUD operations a developer must write code for instance to insert a new record and write code to get the last primary key to provide a incremented key for the new record. A json data source is really only good for one person for personal use but why not use a database as all the features for performing CRUD operations are there at your fingertips.

## Using Dapper

Dapper is an (ORM) library for .NET and .NET Core applications. Dapper is generally faster than EF Core because it uses raw SQL queries and has less overhead. Unlike conventional data operations which require a connection and command object, Dapper does not require a command object to perform data operations.

Simple SELECT example using a managed data provider which using a connection and command objects to loop through returning data into a list.

```csharp
public List<Item> GetAll()
{
    List<Item> list = new List<Item>();
    var statement = """
                    SELECT [Id],[Name],[Description]
                    FROM [dbo].[Item]
                    """;

    using SqlConnection cn = new(ConnectionString());
    using SqlCommand cmd = new() { Connection = cn, CommandText = statement };
    cn.Open();
    var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        list.Add(new Item()
        {
            Id = reader.GetInt32(0), 
            Name = reader.GetString(1), 
            Description = reader.GetString(2)
        });
    }
    return list;
}
```
Performing the same operation with Dapper, much less code.

```csharp
public List<Item> GetAll()
    => cn.Query<Item>(
        """
        SELECT [Id],[Name],[Description] 
        FROM [dbo].[Item]
        """).ToList();
```

Once a developer goes from a managed data provider using a connection and command object to Dapper there is no going back to using a connection and command object.

## Using EF Core

Dependent on the complexity of the database when starting out consider using [EF Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools) which allows a developer to automate reverse engineering a database.

If writing SQL statement is not for you, then EF Core does not require a developer to write SQL. And when using EF Power Tools all a developer needs to do is write client side code. For instance, to get all records in a class for data operations.

```csharp
internal class EntityOperations
{
    public static List<Item> GetAll()
    {
        using var context = new Context();
        return context.Item.ToList();
    }
```

Calling the above.

```csharp
var list = EntityOperations.GetAll();
```

> **Note**
> Dapper and EF Core allow returning less in regards to properties so a developer need not have to return all properties.


## Comparing Dapper to EF Core

In some cases there compare may not match completely where some may be synchronous while others asynchronous.

Model used by both Dapper and EF Core

```csharp
public partial class Item
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    public override string ToString() => Name;
}
```

**EF Core get all records.**

```csharp
public static List<Item> GetAll()
{
    using var context = new Context();
    return context.Item.ToList();
}
```

**Dapper get all records.**

```csharp
public List<Item> GetAll()
    => cn.Query<Item>(
        """
        SELECT [Id],[Name],[Description] 
        FROM [dbo].[Item]
        """).ToList();
```

**EF Core add new record**

```csharp
public static async Task<bool> Add(Item item)
{
    await using var context = new Context();
    context.Add(item);
    return await context.SaveChangesAsync() == 1;
}
```

**Dapper add new record**

```csharp
public async Task Add(Item item)
{
    var identifier = await cn.QueryFirstAsync<int>(
        """
            INSERT INTO [dbo].[Item] ([Name],[Description]) 
            VALUES (@Name,@Description);
            SELECT CAST(scope_identity() AS int);
            """, item);
    item.Id = identifier;
}
```

> **Note**
> When working with Dapper, the SQL written for the accompanying code samples in the GitHub repository were all written in SSMS (SQL-Server Management Studio) to ensure that they worked. This important any time SQL is used in an application. Many novice developers write SQL directly in code and the SQL is poorly written and they have no idea why an exception is raised or results are not as expected.

