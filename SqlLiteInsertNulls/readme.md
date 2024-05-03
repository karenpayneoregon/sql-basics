# About

Simple example for inserting null values with SQLite and Dapper.


Table structure, Column1 through Column5 are nullable.

```sql
CREATE TABLE InsertExample (
    Id      INTEGER PRIMARY KEY AUTOINCREMENT,
    Column1 INTEGER,
    Column2 INTEGER,
    Column3 INTEGER,
    Column4 TEXT,
    Column5 TEXT
);
```

Model for table

```csharp
public class InsertExample
{
    public int Id { get; set; }
    public int? Column1 { get; set; }
    public int? Column2 { get; set; }
    public int? Column3 { get; set; }
    public string? Column4 { get; set; }
    public string? Column5 { get; set; }
}
```

Mocked data

```csharp
public class MockedData
{
    public static List<InsertExample> GetMockedData() =>
    [
        new() { Column1 = null, Column2 = 2, Column3 = 3, Column4 = "A", Column5 = null },
        new() { Column1 = 4, Column2 = null, Column3 = 6, Column4 = null, Column5 = "D" },
        new() { Column1 = 7, Column2 = 8, Column3 = 9, Column4 = "E", Column5 = "F" },
        new() { Column1 = 28, Column2 = 29, Column3 = 30, Column4 = "S", Column5 = "T" }
    ];
}
```

Code to perform inserts

```csharp
internal class DapperOperations
{
    private static string ConnectionString()
        => "Data Source=sample1.db";
    public static int InsertData(List<InsertExample> list)
    {
        using var cn = new SQLiteConnection(ConnectionString());
        return cn.Execute(
            """
            INSERT INTO InsertExample (Column1,Column2,Column3,Column4,Column5)
            VALUES (@Column1,@Column2,@Column3,@Column4,@Column5);
            """, list);
    }
}
```
