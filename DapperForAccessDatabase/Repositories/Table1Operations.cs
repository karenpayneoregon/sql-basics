using Dapper;
using DapperForAccessDatabase.Models;
using System.Data;
using System.Data.OleDb;

namespace DapperForAccessDatabase.Repositories;
internal class Table1Operations
{
    private IDbConnection _cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb");
    public void Add(Table1 sender)
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
        _cn.Execute(statement, new { sender.Column1 });
    }

    public void Delete(Table1 sender)
    {
        const string statement =
            """
            DELETE FROM Table1 
            WHERE Id = @Id
            """;
        _cn.Execute(statement, new { Id = sender.Id });

    }

    public List<Table1> ReadAllRecords()
    {
        return _cn.Query<Table1>(
                """
                SELECT Id, Column1 
                FROM Table1
                """)
            .AsList();
    }
}