using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace GetStoredProceduresApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Write Stored procedure definitions to a file";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
