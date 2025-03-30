using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;


// ReSharper disable once CheckNamespace
namespace AlternateImageApp;
internal partial class Program
{
    /// <summary>
    /// Initializes the application by setting up the console environment and invoking necessary configurations.
    /// </summary>
    /// <remarks>
    /// This method is marked with the <see cref="ModuleInitializerAttribute"/> to ensure it is executed automatically 
    /// before the application starts. It configures the console title, positions the console window, and calls the 
    /// <c>Setup</c> method to initialize application services.
    /// </remarks>
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        Setup();
    }
    /// <summary>
    /// Configures and initializes the necessary services for the application.
    /// </summary>
    /// <remarks>
    /// This method sets up the dependency injection container, retrieves the required services,
    /// and initializes application-specific settings such as connection strings and entity configurations.
    /// </remarks>
    private static void Setup()
    {
        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<SetupServices>();
        setup.GetConnectionStrings();
    }
}
