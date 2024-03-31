using SqlLiteSampleTime.Classes;

namespace SqlLiteSampleTime;

internal partial class Program
{
    static void Main(string[] args)
    {
        DapperOperations.Read();
        Console.ReadLine();
    }
}