using DapperSimpleEfCoreApp.Properties;

namespace DapperSimpleEfCoreApp.Classes;
public class Dialogs
{
    public static bool Question(string caption, string heading, string yesText, string noText, DialogResult defaultButton)
    {

        TaskDialogButton yesButton = new(yesText) { Tag = DialogResult.Yes };
        TaskDialogButton noButton = new(noText) { Tag = DialogResult.No };

        TaskDialogButtonCollection buttons = new();

        if (defaultButton == DialogResult.Yes)
        {
            buttons.Add(yesButton);
            buttons.Add(noButton);
        }
        else
        {
            buttons.Add(noButton);
            buttons.Add(yesButton);
        }

        TaskDialogPage page = new()
        {
            Caption = caption,
            SizeToContent = true,
            Heading = heading,
            Icon = new TaskDialogIcon(Resources.question32),
            Buttons = buttons
        };


        TaskDialogButton result = TaskDialog.ShowDialog(page);

        return (DialogResult)result.Tag! == DialogResult.Yes;

    }
}
