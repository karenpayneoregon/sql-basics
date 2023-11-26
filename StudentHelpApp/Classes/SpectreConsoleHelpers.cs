namespace StudentHelpApp.Classes;
internal class SpectreConsoleHelpers
{
    public static Table CreateViewTable(string text) =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Aqua)
            .Alignment(Justify.Center)
            .Title($"[yellow][B]{text} Items[/][/]")
            .AddColumn(new TableColumn("[cyan]Id[/]"))
            .AddColumn(new TableColumn("[cyan]Name[/]"))
            .AddColumn(new TableColumn("[cyan]Description[/]"));
    public static void ExitPrompt()
    {
        Console.WriteLine();

        Render(new Rule($"[yellow]Press[/] [cyan]ENTER[/] [yellow]to exit the demo[/]")
            .RuleStyle(Style.Parse("silver")).Centered());

        Console.ReadLine();
    }
    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }
}

