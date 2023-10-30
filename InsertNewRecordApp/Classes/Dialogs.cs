using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertNewRecordApp.Classes;
internal class Dialogs
{
    /// <summary>
    /// Windows Forms dialog to ask a question
    /// </summary>
    /// <param name="owner">control or form</param>
    /// <param name="heading">text for dialog heading</param>
    /// <param name="icon">Icon to display</param>
    /// <param name="defaultButton">Button to focus</param>
    /// <returns>true for yes button, false for no button</returns>
    public static bool Question(Control owner, string heading, Icon icon, DialogResult defaultButton = DialogResult.No)
    {

        TaskDialogButton yesButton = new("Yes") { Tag = DialogResult.Yes };
        TaskDialogButton noButton = new("No") { Tag = DialogResult.No };

        var buttons = new TaskDialogButtonCollection();

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
            Caption = "Question",
            SizeToContent = true,
            Heading = heading,
            Icon = new TaskDialogIcon(icon),
            Buttons = buttons
        };

        var result = TaskDialog.ShowDialog(owner, page);

        return (DialogResult)result.Tag! == DialogResult.Yes;

    }
}
