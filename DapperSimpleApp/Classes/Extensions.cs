using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DapperSimpleApp.Classes
{
    internal static class Extensions
    {
        /// <summary>
        /// Provides access to all controls on a form. 
        /// </summary>
        /// <remarks>
        /// See wrapper below <see cref="TextBoxList"/>
        /// </remarks>
        public static IEnumerable<T> Descendants<T>(this Control control) where T : class
        {
            foreach (Control child in control.Controls)
            {
                if (child is T thisControl)
                {
                    yield return (T)thisControl;
                }

                if (child.HasChildren)
                {
                    foreach (T descendant in Descendants<T>(child))
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// Get all <see cref="TextBox"/> in a control which can be a form or a panel for instance.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static List<TextBox> TextBoxList(this Control control)
            => control.Descendants<TextBox>().ToList();

        /// <summary>
        /// Used to split on uppercase characters e.g. FirstName to First Name
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static string SplitOnCapitalLetters(this string sender) 
            => string.Join(" ", Regex.Matches(sender, @"([A-Z][a-z]+)")
                .Cast<Match>()
                .Select(m => m.Value));
    }
}
