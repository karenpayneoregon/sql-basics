namespace DapperPersonRepository.Classes;
public class SpectreConsoleHelpers
{
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

    public static Table PersonTable()
    {
        Table table = new Table()
            .RoundedBorder()
            .AddColumn("[b]Id[/]")
            .AddColumn("[b]First name[/]")
            .AddColumn("[b]Last name[/]")
            .AddColumn("[b]Birth date[/]")
            .BorderColor(Color.LightSlateGrey)
            .LeftAligned().HideHeaders();

        return table;
    }
}
