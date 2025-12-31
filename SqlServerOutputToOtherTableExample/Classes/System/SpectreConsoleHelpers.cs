using System.Runtime.CompilerServices;
using System.Text;
using OED_Console2026.Classes.System;
using SqlServerOutputToOtherTableExample.Classes.System.Models;

namespace SqlServerOutputToOtherTableExample.Classes.System;

public static class SpectreConsoleHelpers
{
    /// <summary>
    /// Sets and displays a styled window title in the console using Spectre.Console.
    /// </summary>
    /// <param name="alignment">The alignment of the title within the console.</param>
    /// <param name="text">
    /// The title text to display. Defaults to "Home screen" if no value is provided.
    /// </param>
    /// <remarks>
    /// This method uses a custom <see cref="Pill"/> element with the <see cref="PillType.Info"/> style
    /// to render the title in a visually appealing format.
    /// </remarks>
    public static void WindowTitle(Justify alignment, string text = "Home screen")
    {

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(alignment)
            .AddColumn("")
            .AddRow(new Pill(text, PillType.Info)));

    }

    /// <summary>
    /// Displays an informational pill-shaped UI element in the console.
    /// </summary>
    /// <param name="alignment">
    /// Specifies the alignment of the pill within the console. 
    /// </param>
    /// <param name="text">
    /// The text to display inside the informational pill. Defaults to "Information" if not specified.
    /// </param>
    /// <remarks>
    /// This method uses the <see cref="Pill"/> class with the <see cref="PillType.Info"/> type
    /// to render a blue informational pill in the console. The pill is displayed within a centered
    /// table layout using Spectre.Console.
    /// </remarks>
    public static void InfoPill(Justify alignment, string text = "Information")
    {

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(alignment)
            .AddColumn("")
            .AddRow(new Pill(text, PillType.Info)));

    }
    /// <summary>
    /// Displays a success pill in the console with the specified alignment and text.
    /// </summary>
    /// <param name="alignment">The alignment of the pill within the console output.</param>
    /// <param name="text">The text to display inside the success pill. Defaults to "Success".</param>
    /// <remarks>
    /// This method uses the <see cref="Pill"/> class with a <see cref="PillType.Success"/> style
    /// to render a visually styled success indicator in the console.
    /// </remarks>
    public static void SuccessPill(Justify alignment, string text = "Success")
    {

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(alignment)
            .AddColumn("")
            .AddRow(new Pill(text, PillType.Success)));

    }
    /// <summary>
    /// Displays an error pill-shaped UI element in the console with the specified alignment and text.
    /// </summary>
    /// <param name="alignment">
    /// The alignment of the error pill within the console. 
    /// </param>
    /// <param name="text">
    /// The text to display inside the error pill. Defaults to "Error occurred" if not specified.
    /// </param>
    /// <remarks>
    /// This method renders a red pill-shaped UI element using the <see cref="Pill"/> class with 
    /// <see cref="PillType.Error"/> to indicate an error message.
    /// </remarks>
    public static void ErrorPill(Justify alignment, string text = "Error occurred")
    {

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(alignment)
            .AddColumn("")
            .AddRow(new Pill(text, PillType.Error)));

    }

    /// <summary>
    /// Displays an error pill in the console with a specified alignment, text, and exception details.
    /// </summary>
    /// <param name="alignment">The alignment of the pill within the console output.</param>
    /// <param name="text">The text to display inside the error pill.</param>
    /// <param name="exception">The exception whose message will be displayed alongside the error pill.</param>
    /// <remarks>
    /// This method renders a styled error pill along with the exception message using Spectre.Console.
    /// The pill is styled based on the <see cref="PillType.Error"/> type.
    /// </remarks>
    public static void ErrorPill(Justify alignment, string text, Exception exception)
    {

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(alignment)
            .AddColumn("")
            .AddColumn("")
            .AddRow(new Pill(text, PillType.Error), new Text(exception.Message)));

    }

    /// <summary>
    /// Displays a warning pill-shaped UI element in the console with the specified alignment and text.
    /// </summary>
    /// <param name="alignment">The alignment of the warning pill within the console layout.</param>
    /// <param name="text">The text to display inside the warning pill. Defaults to "Warning".</param>
    /// <remarks>
    /// The warning pill is rendered using the <see cref="Pill"/> class with a <see cref="PillType.Warning"/> style.
    /// It is displayed within a centered table layout using Spectre.Console.
    /// </remarks>
    public static void WarningPill(Justify alignment, string text = "Warning")
    {

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(alignment)
            .AddColumn("")
            .AddRow(new Pill(text, PillType.Warning)));

    }
    /// <summary>
    /// Displays a prompt in the console indicating that the user can press any key to exit.
    /// </summary>
    /// <remarks>
    /// This method hides the cursor, adds a blank line for spacing, and renders a centered table
    /// with a message using Spectre.Console. It waits for the user to press any key before continuing.
    /// </remarks>
    public static void ExitPrompt(Justify alignment = Justify.Center)
    {
        Console.CursorVisible = false;
        Console.WriteLine();

        AnsiConsole.Write(new Table()
            .Border(TableBorder.None)
            .Alignment(alignment)
            .AddColumn("").AddColumn("")
            .AddRow(new Pill("Press any key to exit...", PillType.Info), new Text("")));

        Console.ReadLine();
    }

    /// <summary>
    /// Configures the console's input and output encoding to use UTF-8.
    /// </summary>
    /// <remarks>
    /// This method sets the console's output encoding to UTF-8 without emitting a BOM (Byte Order Mark),
    /// and the input encoding to UTF-8.
    /// </remarks>
    public static void SetEncoding()
    {
        Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        Console.InputEncoding = Encoding.UTF8;
    }


    public static void PrintCyan([CallerMemberName] string methodName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]{methodName}[/]");
        Console.WriteLine();
    }


    /// <summary>
    /// Prints a formatted message to the console in pink color, including the project name, file name, and method name.
    /// </summary>
    /// <param name="filePath">
    /// The file path of the caller. This parameter is automatically populated by the compiler.
    /// </param>
    /// <param name="methodName">
    /// The name of the calling method. This parameter is automatically populated by the compiler.
    /// </param>
    public static void PrintPink([CallerFilePath] string filePath = null, [CallerMemberName] string methodName = null)
    {

        // Get file and project name
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var projectName = Utilities.GetProjectName(filePath);

        AnsiConsole.MarkupLine($"[hotpink2]{projectName}[/][yellow bold].[/][hotpink2]" +
                               $"{fileName}[/][yellow bold].[/][hotpink2]{methodName}[/]");

        Console.WriteLine();
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
}