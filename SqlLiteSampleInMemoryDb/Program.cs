using Microsoft.Data.Sqlite;
using SqlLiteSampleInMemoryDb.Classes;

namespace SqlLiteSampleInMemoryDb;

internal partial class Program
{
    static void Main(string[] args)
    {
        DapperOperations.Execute();
        Console.ReadLine();
    }
}