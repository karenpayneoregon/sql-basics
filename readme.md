# Tips on interacting with a database with C#

This article main intent is to provide useful information for interating with data using a managed data provider, adding [Dapper](https://dapperlib.github.io/Dapper/) to the mix for new developers to get the basics which in turn allows a developer to write clean reusable code.

Some developers might want to jump into [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) which is fine but understanding the basic is going to be helpful when diving into Entity Framework Core.

Which is better, data provider or Entity Framework Core? It depends on the task and performance expected. The author uses both.

## Writing your first SELECT statement

The average developer creates a database with a table or perhaps a set of tables then writes an SQL statement to select records as shown below.

:heavy_check_mark: Connection string is an issue we will address later

The developer saw others on the web cancatenating an SQL statement WHERE condition and heard it worked so they write the following code and pass in **Brown** for the WHERE condition. In this case **Brown** exist and the primary key is shown.


```csharp
static void Playground(string lastName)
{

    string connectionString = 
        "Data Source=(localdb)\\MSSQLLocalDB;" + 
        "Initial Catalog=NorthWind2022;Integrated Security=True";

    using var cn = new SqlConnection(connectionString);
    using var cmd = new SqlCommand
    {
        Connection = cn,
        CommandText = "SELECT ContactId FROM  dbo.Contacts WHERE  (LastName = '" + lastName +"')"
    };

    cn.Open();

    var reader = cmd.ExecuteReader();
    if (reader.HasRows)
    {
        reader.Read();
        Console.WriteLine(reader.GetInt32(0));
    }
    else
    {
        Console.WriteLine("No matches");
    }
}
```

So what happens if **Roel's** is used? Upon executing the code we get

```
Microsoft.Data.SqlClient.SqlException: 'Incorrect syntax near 's'.
Unclosed quotation mark after the character string ')'.'
```

The resolution, use [parameters](https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.parameters?view=dotnet-plat-ext-7.0), all data providers have parameters.

For SQL-Sever in SSMS (SQL-Server Management Studio) a parameter begin with **@**


```sql
DECLARE @LastName AS NCHAR(50) = N'Roel''s';

SELECT ContactId,
       FirstName,
       LastName
FROM dbo.Contacts
WHERE (LastName = @LastName);
```

In SSMS we must escape an apostrophe as shown above while when using parameters in C# with the command object, we create a new parameter with **Parameters.Add** where the first parameter in the name of the parameter (in SSMS DECLARE), the second parameter is the type, in this case for string **SqlDbType.NVarChar** then tag on .Value which here is `lastname`. Internally the code escapes the apostrophe.

New updated version of code

```csharp
static void Playground(string lastName)
{

    string connectionString = 
        "Data Source=(localdb)\\MSSQLLocalDB;" + 
        "Initial Catalog=NorthWind2022;Integrated Security=True";

    using var cn = new SqlConnection(connectionString);
    using var cmd = new SqlCommand
    {
        Connection = cn,
        CommandText = "SELECT ContactId FROM  dbo.Contacts WHERE  (LastName = @LastName)"
    };

    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
    Console.WriteLine(cmd.CommandText);
    cn.Open();

    var reader = cmd.ExecuteReader();
    if (reader.HasRows)
    {
        reader.Read();
        Console.WriteLine(reader.GetInt32(0));
    }
    else
    {
        Console.WriteLine("No matches");
    }
}
```

Note the line **Console.WriteLine(cmd.CommandText);** which writes out.

```sql
SELECT ContactId FROM  dbo.Contacts WHERE  (LastName = @LastName)
```

We do not see the actual value for LastName, this is a bonus as there is no room for [SQL-Injection](https://owasp.org/www-community/attacks/SQL_Injection).

So how do we know if the statement is good? We test by writing the statement in SSMS and we can also run my NuGet package [DbPeekQueryLibrary](https://www.nuget.org/packages/DbPeekQueryLibrary/).

```csharp
cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
Console.WriteLine(cmd.ActualCommandText());
cn.Open();
```

Which shows

```sql
SELECT ContactId FROM  dbo.Contacts WHERE  (LastName = 'Roel''s')
```

> **Note**
> When adding parameters as shown above, the order they are added does not matter. If we were working with MS-Access the parameter order matters. If we were working with Oracle, the default is same as MS-Access but using BindByName = true on the command object parameter order does not matter, same as SQL-Server.

The following example uses a larger SELECT with several JOINS, two WHERE conditions and the parmeters are out of order which is fine as they are by name not ordinal.

```csharp
static void Playground2(int countryIdentifier, int contactTypeIdentifier)
{

    string connectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;" +
        "Initial Catalog=NorthWind2022;Integrated Security=True";

    using var cn = new SqlConnection(connectionString);
    using var cmd = new SqlCommand
    {
        Connection = cn,
        CommandText = """
            SELECT C.CustomerIdentifier,
                   C.CompanyName,
                   C.ContactId,
                   C.Street,
                   C.City,
                   C.PostalCode,
                   C.CountryIdentifier,
                   C.Phone,
                   C.ContactTypeIdentifier,
                   CT.ContactTitle,
                   Cont.FirstName,
                   Cont.LastName
            FROM dbo.Customers AS C
                INNER JOIN dbo.Countries AS A
                    ON C.CountryIdentifier = A.CountryIdentifier
                INNER JOIN dbo.ContactType AS CT
                    ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                INNER JOIN dbo.Contacts AS Cont
                    ON C.ContactId = Cont.ContactId
                       AND CT.ContactTypeIdentifier = Cont.ContactTypeIdentifier
            WHERE (C.CountryIdentifier = @CountryIdentifier)
                  AND (C.ContactTypeIdentifier = @ContactTypeIdentifier);
            """
    };

    cmd.Parameters.Add("@ContactTypeIdentifier", SqlDbType.NVarChar).Value = contactTypeIdentifier;
    cmd.Parameters.Add("@CountryIdentifier", SqlDbType.NVarChar).Value = countryIdentifier;
    Console.WriteLine(cmd.ActualCommandText());
    cn.Open();

    var reader = cmd.ExecuteReader();
    if (reader.HasRows)
    {
        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader.GetInt32(0),-4} Company: {reader.GetString(1)}");
        }
        
    }
    else
    {
        Console.WriteLine("No matches");
    }
}
```

## About connections

Since a developer could interact with database there are many who create a global connection, when needed check if the connection is open, if not open and use the connection.

Rather go into all the reason this is bad, the recommended approach is to create a new connection when needed and once finished dispose of the connection.

Each of the examples follow this approach. 

## About connection strings

DO NOT store connection string in each **SqlConnection**, for ASP.NET Core and Razor Pages we can store the connection string in a json file typically named **appsettings.json**.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NorthWind2022;Integrated Security=True"
  }
}
```

And in Program Main have acccess via

```csharp
builder.Configuration.GetConnectionString("DefaultConnection")
```

For other project types create a model for appsettings.json and read the settings or use my NuGet package [ConfigurationLibrary](https://www.nuget.org/packages/ConfigurationLibrary/).

Sample appsettings.json

```json
{
  "ConnectionsConfiguration": {
    // do not get hung up on the next line it comes from allowing multiple environments
    "ActiveEnvironment": "Development",
    "Development": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NorthWind2022;Integrated Security=True;Encrypt=False"
  }
}
```

Add the following using to the class that works with a database.

```csharp
using static ConfigurationLibrary.Classes.ConfigurationHelper;
```

Create a new connection passing **ConnectionString()** which reads te Development item under ConnectionsConfiguration.

```csharp
using var cn = new SqlConnection(ConnectionString());
```

> **Note**
> Go to the package source which explains having three connection strings, development, stagging and production.


## Placement of SQL statements

In all the example above the SQL statements were inline with the command object, a better idea is to create a class, in this case SqlStatements.cs and place the SQL in there.

In the last code sample the SQL took up a lot of space and can only be used in that method.

Here we have a better solution, cleans up your code and is reusable.


```csharp
public class SqlStatements
{
    public static string CustomersByContactTypeAndCountry()
        => """
                SELECT C.CustomerIdentifier,
                       C.CompanyName,
                       C.ContactId,
                       C.Street,
                       C.City,
                       C.PostalCode,
                       C.CountryIdentifier,
                       C.Phone,
                       C.ContactTypeIdentifier,
                       CT.ContactTitle,
                       Cont.FirstName,
                       Cont.LastName
                FROM dbo.Customers AS C
                    INNER JOIN dbo.Countries AS A
                        ON C.CountryIdentifier = A.CountryIdentifier
                    INNER JOIN dbo.ContactType AS CT
                        ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                    INNER JOIN dbo.Contacts AS Cont
                        ON C.ContactId = Cont.ContactId
                           AND CT.ContactTypeIdentifier = Cont.ContactTypeIdentifier
                WHERE (C.CountryIdentifier = @CountryIdentifier)
                      AND (C.ContactTypeIdentifier = @ContactTypeIdentifier);
                """;
}
```

Cleaner code

```csharp
using var cn = new SqlConnection(ConnectionString());
using var cmd = new SqlCommand
{
    Connection = cn,
    CommandText = SqlStatements.CustomersByContactTypeAndCountry()

};

cmd.Parameters.Add("@ContactTypeIdentifier", 
    SqlDbType.NVarChar).Value = contactTypeIdentifier;
cmd.Parameters.Add("@CountryIdentifier", 
    SqlDbType.NVarChar).Value = countryIdentifier;

cn.Open();
```

## Reading back data

When there are more than one record we use **reader.HasRows** to see if there are any records then use a while to iterate data. DO NOT use this if only one record is being returned.

```csharp
var reader = cmd.ExecuteReader();
if (reader.HasRows)
{
    while (reader.Read())
    {
        Console.WriteLine($"Id: {reader.GetInt32(0),-4} Company: {reader.GetString(1)}");
    }
}
```

For returning a single record, see if there is data then if there is use the Read method of the reader.

```csharp
var reader = cmd.ExecuteReader();
if (reader.HasRows)
{
    reader.Read();
    Console.WriteLine(reader.GetInt32(0));
}
```

## Storing/returning data

In each of the example above all that was done was display data to the console window. 

One option is to return data into a [DataTable](https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-7.0). DataTables are okay for desktop projects but generally are too heavy for modern day web applications.

Example which the caller passing an int and we get a DataTable back.

```csharp
static DataTable Playground1DataTable(int top)
{

    DataTable dt = new DataTable();

    using var cn = new SqlConnection(ConnectionString());
    using var cmd = new SqlCommand
    {
        Connection = cn,
        CommandText = SqlStatements.ContactDemo()
    };

    cmd.Parameters.Add("@Top", SqlDbType.NVarChar).Value = top;

    cn.Open();

    dt.Load(cmd.ExecuteReader());
    return dt;
}
```

Another option, create a model/class which has properties for each column in a SELECT statement.

The following example uses Dapper.

SELECT statement

```csharp
public class SqlStatements
{
    public static string ContactDemo() =>
        """
        SELECT  ContactId,
                FirstName,
                LastName,
                ContactTypeIdentifier  
        FROM dbo.Contacts 
        WHERE ContactId > @Top
        """;
```

Model to match the SELECT statement

```csharp
public class Contact
{
    public int ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ContactTypeIdentifier { get; set; }
    public override string ToString() => LastName;
}
```

Code to read

```csharp
static List<Contact> Playground1Dapper(int top)
{

    using SqlConnection cn = new (ConnectionString());
    // specify our parameter for the WHERE condition
    var parameters = new { @Top = top };

    List<Contact> list = cn.Query<Contact>(
        SqlStatements.ContactDemo(), 
        parameters).ToList();


    return list;
}
```

## Multiple Result Sets for SQL-Server

Learn how to read reference table from SQL-Server using a single method. What is shown provides an efficient way to either a connection, command objects to read data via a SqlDataReader for conventional work using methods from SqlClient and also Dapper which requires two lines of code to read data and one line of code to store data into list.

### Goal

To read from three reference table in a modified version of Microsoft NorthWind database, Categories, ContactType and Countries tables.

In all code samples all records are read from each table, in some cases not all records may be needed, simply change the SQL SELECT statement with a WHERE clause. Also, the same goes for columns.

### Project

See [GitHubSamples](https://github.com/karenpayneoregon/sql-basics/tree/master/NextResultsApp) (name came from another of this author's work)


## Provides code samples

There is one console project uses to write all the code samples which appeared in this article. All code is in this project.

There is a Windows Form project where unlike the console project, all data opertions are in a class project.

And finally a project which uses Entity Framework which is another console project were all dta operations are in other class project. 

## Preparing to run code

- Make sure to use Microsoft Visual Studio 2022
- Run both scripts in the root of the solution

## Source code

Clone the following [GitHub repository](https://github.com/karenpayneoregon/sql-basics).