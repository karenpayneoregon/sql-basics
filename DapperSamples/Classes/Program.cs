using System.Runtime.CompilerServices;
using ConsoleHelperLibrary.Classes;
using Spectre.Console;

// ReSharper disable once CheckNamespace
namespace DapperSamples;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Dapper code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }



}
