using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace TransferFromJsonToDatabaseToExcel;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.SetWindowSize(60, 10);
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
