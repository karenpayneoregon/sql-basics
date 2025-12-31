using Microsoft.Data.SqlClient;
using Dapper;

namespace SqlServerOutputToOtherTableExample.Classes;
internal class RefreshData
{
    public static void Reset()
    {
        using SqlConnection cn = new(DataConnections.Instance.MainConnection);
        
        cn.Execute("DELETE FROM dbo.Person");
        cn.Execute("DBCC CHECKIDENT (Person, RESEED, 0)");

        cn.Execute("DELETE FROM dbo.[Transactions]");
        cn.Execute("DBCC CHECKIDENT ([Transactions], RESEED, 0)");
    }
}
