using System.Data.SQLite;
using Dapper;

namespace SqlLiteInsertNulls.Classes;
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
