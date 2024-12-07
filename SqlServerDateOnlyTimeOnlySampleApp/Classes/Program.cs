using Microsoft.Extensions.DependencyInjection;
using SqlServerDateOnlyTimeOnlySampleApp.Classes.Configuration;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace SqlServerDateOnlyTimeOnlySampleApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Fill);
    }
    private static async Task Setup()
    {
        var services = ApplicationConfiguration.ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<SetupServices>()!.GetConnectionStrings();
    }
}
