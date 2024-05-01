using Dapper;
using System.Data;
using System.Data.OleDb;

namespace DapperForAccessDatabase.Repositories;
internal class Table1Operations
{
    private IDbConnection _cn = new OleDbConnection(
        @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb");
    public void Add(Table1 person)
    {
        const string statement =
            """
            INSERT INTO Table1
            (
                Column1
            )
            VALUES
            (
                @Column1
            )
            """;
        _cn.Execute(statement, new { person.Column1 });
    }
}

public class Table1
{
    public int Id { get; set; }
    public string Column1 { get; set; }
}
