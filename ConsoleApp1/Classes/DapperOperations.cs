using Microsoft.Data.SqlClient;
using System.Data;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace ConsoleApp1.Classes;
internal class DapperOperations
{
    private IDbConnection cn;

    public DapperOperations()
    {
        cn = new SqlConnection(ConnectionString());
    }
}
