using AlternateImageApp.Classes;

namespace AlternateImageApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        var list = FileOperations.ReadAltTexts(@"ImageText\ImageAltText.txt");
        DataOperations.AddRange(list, @"Images");
        DataOperations.Write();

        AnsiConsole.MarkupLine("[yellow]Hello[/]");
        Console.ReadLine();
    }
}