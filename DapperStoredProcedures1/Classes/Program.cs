using DapperStoredProcedures1.Classes;
using Dumpify;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace DapperStoredProcedures1;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
        DumpConfig.Default.ColorConfig.TypeNameColor = System.Drawing.Color.GreenYellow;
        DumpConfig.Default.ColorConfig.LabelValueColor = System.Drawing.Color.BlueViolet;
        DumpConfig.Default.TableConfig.ShowTableHeaders = false;
    }
    private static async Task Setup()
    {
        var services = ApplicationConfiguration.ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<SetupServices>()!.GetConnectionStrings();
    }
}
