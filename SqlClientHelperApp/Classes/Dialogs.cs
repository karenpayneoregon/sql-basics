using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlClientHelperApp.Classes;
internal class Dialogs
{
    public static void Information(Control owner, string heading, string text, string buttonText = "Ok")
    {

        TaskDialogButton okayButton = new(buttonText);

        TaskDialogPage page = new()
        {
            Caption = "Information",
            SizeToContent = true,
            Heading = heading,
            Footnote = new TaskDialogFootnote() { Text = "Code sample by Karen Payne" },
            Text = text,
            Icon = new TaskDialogIcon(new Bitmap(new MemoryStream(Properties.Resources.blueInformation_32))), 
            Buttons = [okayButton]
        };

        TaskDialog.ShowDialog(owner, page);

    }
}
