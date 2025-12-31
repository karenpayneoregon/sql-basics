using Microsoft.Extensions.DependencyInjection;
using SqlServerOutputToOtherTableExample.Classes.System;
using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace SqlServerOutputToOtherTableExample;
internal partial class Program
{

    [ModuleInitializer]
    public static void Init()
    {
        SpectreConsoleHelpers.SetEncoding();

        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
        Console.Title = product!;
        
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
    private static async Task Setup()
    {
        var services = ApplicationConfiguration.ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<SetupServices>()!.GetConnectionStrings();
    }
}
