using TransferFromJsonToDatabaseToExcel.Classes;
using static TransferFromJsonToDatabaseToExcel.Classes.SpectreConsoleHelpers;

namespace TransferFromJsonToDatabaseToExcel;

internal partial class Program
{
    static Task Main(string[] args)
    {

        CrudeService.Message += OnMessageReceived;
        AnsiConsole.MarkupLine("[cyan]Reading mocked data[/]");
        CrudeService.Start();
        /*
         * Here the timer runs to completion on user aborts
         * by pressing ENTER.
         */
        Console.ReadLine();
       
        return Task.CompletedTask;
    }

    private static void OnMessageReceived(string message)
    {
        if (message.Contains("Exported from database to Excel"))
        {
            AnsiConsole.MarkupLine($"{message} - Done");
            ExitPrompt();
        }
        else
        {
            AnsiConsole.MarkupLine(message);
        }
    }
}