
namespace TransferFromJsonToDatabaseToExcel.Classes;

public class SpectreConsoleHelpers
{
    public static void ExitPrompt()
    {
        Console.WriteLine();

        AnsiConsole.MarkupLine("[yellow]Press[/] [cyan]ENTER[/] [yellow]to exit the demo[/]");
        Console.ReadLine();
    }

}