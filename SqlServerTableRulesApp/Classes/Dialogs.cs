namespace SqlServerTableRulesApp.Classes;
internal class Dialogs
{
    /// <summary>
    /// Displays an informational dialog with a specified heading and an optional button text.
    /// </summary>
    /// <param name="owner">
    /// The control or form that owns the dialog. This determines the dialog's parent window.
    /// </param>
    /// <param name="heading">
    /// The heading text to display in the dialog.
    /// </param>
    /// <param name="buttonText">
    /// The text to display on the dialog's button. Defaults to "Ok" if not specified.
    /// </param>
    public static void Information(Control owner, string heading, string buttonText = "Ok")
    {

        
        TaskDialogButton okayButton = new(buttonText);

        TaskDialogPage page = new()
        {
            Caption = "Information",
            SizeToContent = true,
            Heading = heading,
            Footnote = new TaskDialogFootnote() { Text = "Code sample by Karen Payne" },
            Buttons = [okayButton]
        };

        TaskDialog.ShowDialog(owner, page);

    }
}
