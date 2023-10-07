using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowFilterApp.Extensions;
public static class TextBoxExtensions
{
    /// <summary>
    /// Method base of the function
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this TextBox sender)
    {
        return string.IsNullOrWhiteSpace(sender.Text);
    }
}