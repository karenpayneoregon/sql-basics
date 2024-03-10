using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace SqlLiteSample1;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("[underline green]Dapper/SqlLite sample[/]");
        Console.Title = "Dapper/SqlLite basic code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
