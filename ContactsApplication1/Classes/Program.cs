using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using ContactsApplication1.Classes.Configuration;
using ContactsApplication1.Classes.Core;
using ContactsApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace ContactsApplication1;
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
                services.AddDbContext<Context>(options => { options.UseSqlServer(AppConnections.Instance.MainConnection); });

                services.AddHostedService<EntityCoreWarmupService>();
            })
            .Build();

        await host.StartAsync();
        await host.StopAsync();

    }
}
