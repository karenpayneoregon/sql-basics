using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;
using System.Runtime.CompilerServices;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace DapperBirthdaysComputedColumns;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
    private static void Setup()
    {
        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<SetupServices>();
        setup.GetConnectionStrings();

    }
}
