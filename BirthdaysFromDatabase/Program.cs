using BirthdaysFromDatabase.Classes.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdaysFromDatabase;
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();
        await Setup();
        Application.Run(new MainForm());
    }

    private static async Task Setup()
    {
        var services = Classes.Configuration.ApplicationConfiguration.ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<SetupServices>()!.GetConnectionStrings();
    }
}