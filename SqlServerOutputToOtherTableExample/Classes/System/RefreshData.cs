using Microsoft.Data.SqlClient;
using Dapper;

namespace SqlServerOutputToOtherTableExample.Classes.System;
internal class RefreshData
{
    
    /// <summary>
    /// Resets the data in the database by clearing the contents of the <c>Person</c> 
    /// and <c>Transactions</c> tables and reseeding their identity columns.
    /// </summary>
    /// <remarks>
    /// This method deletes all records from the <c>dbo.Person</c> and <c>dbo.Transactions</c> tables 
    /// and resets their identity values to start from 0. Use this method with caution as it 
    /// irreversibly removes all data from these tables.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue executing the SQL commands.
    /// </exception>
    public static void Reset()
    {
        using SqlConnection cn = new(DataConnections.Instance.MainConnection);
        
        cn.Execute("DELETE FROM dbo.Person");
        cn.Execute("DBCC CHECKIDENT (Person, RESEED, 0)");

        cn.Execute("DELETE FROM dbo.[Transactions]");
        cn.Execute("DBCC CHECKIDENT ([Transactions], RESEED, 0)");
    }
}
