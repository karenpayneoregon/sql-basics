using EnumHasConversion.Classes;
using EnumHasConversion.Data;
using EnumHasConversion.Extensions;
using EnumHasConversion.Models;
using PayneServiceLibrary;
using PayneServiceLibrary.Classes.Configuration;
using static EnumHasConversion.Classes.SpectreConsoleHelpers;

namespace EnumHasConversion;

internal partial class Program
{
    /// <summary>
    /// This method initializes the application by setting up the necessary configurations, 
    /// connecting to the database, and performing operations such as creating or populating 
    /// the database if needed. It also demonstrates retrieving and processing wine data 
    /// and running examples.
    /// </summary>
    private static async Task Main()
    {
        await MainConfiguration.Setup();

        await using var context = new WineContext();

        CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(5));

        var success = context.CanConnectAsync(cancellationTokenSource.Token);
        if (success == false)
        {
            AnsiConsole.MarkupLine("[cyan]Creating and populating database[/]");
            if (EntitySettings.Instance.CreateNew)
            {
                await context.Database.EnsureDeletedAsync(cancellationTokenSource.Token);
                await context.Database.EnsureCreatedAsync(cancellationTokenSource.Token);
                Console.Clear();
            }

        }
        else
        {
            AnsiConsole.MarkupLine("[cyan]Using existing database[/]");
            Console.WriteLine();
        }

        List<Wine> redWines = WineOperations.GetWinesByType(WineType.Red);

        WineOperations.RunExamples();

        ExitPrompt();

    }
}