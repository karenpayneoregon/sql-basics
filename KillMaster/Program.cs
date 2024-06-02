using Dapper;
using Microsoft.Data.SqlClient;

namespace KillMaster;

internal class Program
{
    static void Main(string[] args)
    {
        var connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Encrypt=False";
        using var cn = new SqlConnection(connectionString);
        cn.Execute("KILL 73");
        Console.WriteLine("Done");
        Console.ReadLine();
    }
}
