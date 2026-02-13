using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NorthWindSqlLiteApp1.Classes.Configuration;
using NorthWindSqlLiteApp1.Classes.Core;
using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Services;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace NorthWindSqlLiteApp1;
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

    private static async Task Warmup()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // 🔥 removes all logging
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<Context>(options => { options.UseSqlite(AppConnections.Instance.MainConnection); });

                services.AddHostedService<EfCoreWarmupService>();
            })
            .Build();

        await host.StartAsync();
        await host.StopAsync();

    }
}
