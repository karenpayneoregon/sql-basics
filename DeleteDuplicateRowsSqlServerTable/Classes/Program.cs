using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace DeleteDuplicateRowsSqlServerTable;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }

    public static Table CreateTable()
        => new Table()
            .RoundedBorder()
            .Centered()
            .AddColumn("[cyan]Id[/]")
            .AddColumn("[cyan]First[/]")
            .AddColumn("[cyan]Last[/]")
            .AddColumn("[cyan]Birth[/]")
            .BorderColor(Color.LightSlateGrey)
            .Border(TableBorder.Square)
            .Title("[LightGreen]Data[/]");
}

