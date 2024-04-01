using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace SqlLiteSample2;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample - SqlLite NorthWind2024";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
