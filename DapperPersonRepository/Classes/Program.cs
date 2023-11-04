using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace DapperPersonRepository;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Dapper repository code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }



}
