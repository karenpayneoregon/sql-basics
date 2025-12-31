// required for MonthNames

using System.Globalization;
using Spectre.Console.Rendering;
using SqlServerOutputToOtherTableExample.Classes.System;

namespace SqlServerOutputToOtherTableExample.Classes.SpectreSamples;


/// <summary>
/// Provides a collection of sample methods demonstrating the usage of Spectre.Console 
/// for various console-based interactions.
/// </summary>
/// <remarks>
/// This class includes examples such as multi-selection prompts, numeric input validation, 
/// and user detail collection, showcasing the capabilities of Spectre.Console for creating 
/// interactive and styled console applications.
/// </remarks>
internal class DisplaySamples
{


    /// <summary>
    /// Prompts the user to select their favorite months from a list of all months in the year.
    /// </summary>
    /// <remarks>
    /// This method uses <see cref="Spectre.Console.MultiSelectionPrompt{T}"/> to allow multi-selection 
    /// of months. The selected months are displayed using <see cref="Spectre.Console.AnsiConsole.MarkupLine(string)"/> 
    /// with custom formatting.
    /// </remarks>
    public static void GetFavoriteMonths()
    {
        // Example with multi-selection, in this case, months of the year
        List<string> favoriteMonths = AnsiConsole.Prompt(new MultiSelectionPrompt<string>()
            .Title("Select [green]favorite [/] months:")
            .PageSize(12)
            .AddChoices(Months()));

        // Display the selected months
        foreach (var month in favoriteMonths)
        {
            AnsiConsole.MarkupLine($"[DeepPink1]{month, 15}[/]");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Prompts the user to enter an integer within a specified range using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the use of <see cref="Spectre.Console.TextPrompt{T}"/> to create a 
    /// styled and validated prompt for numeric input. The prompt ensures the entered value is 
    /// between 1 and 10, providing appropriate error messages for invalid inputs.
    /// </remarks>
    /// <example>
    /// The following is an example of the prompt displayed to the user:
    /// <code>
    /// Enter a number between 1 and 10
    /// </code>
    /// If the user enters a value outside the range, an error message such as 
    /// "1 is min value" or "10 is max value" is displayed.
    /// </example>
    public static void GetInteger()
    {
        // Example with validation, get an int between 1 and 10
        var integer = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter a [cyan]number[/] between [b]1[/] and [b]10[/]")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid age[/]")
                .Validate(age => age switch
                {
                    <= 0 => ValidationResult.Error("[red]1 is min value[/]"),
                    >= 10 => ValidationResult.Error("[red]10 is max value[/]"),
                    _ => ValidationResult.Success(),
                }));
    }

    


    #region Example of standard colors and getting a password hidden

    private static string promptColor => "[cyan]";
    private static string inputColor => "white";

    public static string PasswordPrompt(string text) => AnsiConsole.Prompt(
        new TextPrompt<string>($"Enter {promptColor}{text}[/]?")
            .PromptStyle(inputColor)
            .Secret()
            .AllowEmpty());

    #endregion
    
    /// <summary>
    /// Retrieves the list of month names from the current culture's <see cref="DateTimeFormatInfo"/>.
    /// </summary>
    /// <remarks>
    /// This method excludes the empty string typically present at the end of the month names array
    /// and converts the resulting array into a <see cref="List{T}"/>.
    /// </remarks>
    /// <returns>
    /// A <see cref="List{T}"/> of month names from the current culture, excluding the empty string.
    /// </returns>
    private static List<string> Months() 
        => DateTimeFormatInfo.CurrentInfo.MonthNames[..^1].ToList();

    /// <summary>
    /// Demonstrates the creation and display of a table using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method creates a table with two columns: "Index" and "Name". It populates the table
    /// with the indices and names of months retrieved from the current culture's month names.
    /// The table is then displayed in the console using <see cref="Spectre.Console.AnsiConsole.Write(IRenderable)"/>.
    /// </remarks>
    public static void TableExample()
    {
        SpectreConsoleHelpers.PrintCyan();

        var months = DateTimeFormatInfo.CurrentInfo.MonthNames[..^1].ToList();
        
        var table = new Table();
        table.Title("[yellow]Month Names[/]");
        table.AddColumn("[b][u]Index[/][/]");
        table.AddColumn("[b][u]Name[/][/]");

        for (var i = 0; i < months.Count; i++)
        {
            table.AddRow($"{i + 1}", months[i]);
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Demonstrates the usage of various pill-shaped UI elements in the console, 
    /// including error, warning, informational, and success messages.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="SpectreConsoleHelpers"/> class to render 
    /// visually distinct pill elements with different styles and alignments.
    /// </remarks>
    public static void PillExamples()
    {
        SpectreConsoleHelpers.WindowTitle(Justify.Left);
        SpectreConsoleHelpers.ErrorPill(Justify.Left, "Error: Something went wrong");
        SpectreConsoleHelpers.WarningPill(Justify.Left, "Potential issue detected");
        SpectreConsoleHelpers.InfoPill(Justify.Left, "Time to backup data");
        SpectreConsoleHelpers.SuccessPill(Justify.Left, "Operation completed successfully");
    }
}
