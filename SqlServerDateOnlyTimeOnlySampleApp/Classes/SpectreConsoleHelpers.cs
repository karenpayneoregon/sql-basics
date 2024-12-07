using System.Runtime.CompilerServices;

namespace SqlServerDateOnlyTimeOnlySampleApp.Classes;
public static class SpectreConsoleHelpers
{
    public static void ExitPrompt()
    {
        Console.WriteLine();

        Render(new Rule($"[yellow]Press[/] [cyan]ENTER[/] [yellow]to exit the demo[/]")
            .RuleStyle(Style.Parse("silver")).Centered());

        Console.ReadLine();
    }
    public static void PrintCyan([CallerMemberName] string? methodName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]{methodName.Replace("_", " ")}[/]");
        Console.WriteLine();
    }
    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Spectre.Console  Add [ to [ and ] to ] so Children[0].Name changes to Children[[0]].Name
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static string ConsoleEscape(this string sender)
        => Markup.Escape(sender);

    /// <summary>
    /// Spectre.Console Removes markup from the specified string.
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static string ConsoleRemove(this string sender)
        => Markup.Remove(sender);

    /// <summary>
    /// Highlights SQL keywords in the specified string by wrapping them with Spectre.Console markup tags.
    /// </summary>
    /// <param name="sender">The input string containing SQL keywords.</param>
    /// <returns>A string with SQL keywords highlighted using Spectre.Console markup.</returns>
    public static string HighlightKeyWords(this string sender)
    {
        return sender
            .Replace("SELECT", "[cyan]SELECT[/]")
            .Replace("FROM", "[cyan]FROM[/]")
            .Replace("WHERE", "[cyan]WHERE[/]")
            .Replace("CAST", "[cyan]CAST[/]")
            .Replace("AS", "[cyan]AS[/]")
            .Replace("Parameters=", "[yellow]Parameters=[/]")
            .Replace("CROSS APPLY OPENJSON", "[mediumvioletred]CROSS APPLY OPENJSON[/]");
    }
}

