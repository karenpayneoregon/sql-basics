using MaskingSocialApp.Data;

namespace MaskingSocialApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        using (var context = new Context())
        {
            var list = context.Taxpayer.ToList();
            AnsiConsole.MarkupLine("[yellow]Hello[/]");
        }
        Console.ReadLine();
    }
}