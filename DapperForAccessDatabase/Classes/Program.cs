using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace DapperForAccessDatabase;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Dapper: Add records to MS-Access table";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
