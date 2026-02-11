using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using ReverseEngineerNorthWind2024.Classes.Configuration;
using ReverseEngineerNorthWind2024.Classes.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace ReverseEngineerNorthWind2024;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        Setup();

    }
    private static void Setup()
    {

        SetupLogging.Development();

        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<SetupServices>();
        setup.GetConnectionStrings();
        setup.GetEntitySettings();

        SpectreConsoleHelpers.SetEncoding();
    }
}
