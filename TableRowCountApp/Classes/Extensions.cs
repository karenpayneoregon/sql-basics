

using System.Text.RegularExpressions;
using SqlServerLibrary.Models;

namespace TableRowCountApp.Classes;
public static partial class Extensions
{
    /// <summary>
    /// Removes square brackets from the <see cref="TableRow.Name"/> property of each <see cref="TableRow"/>
    /// in the list.
    /// </summary>
    /// <param name="sender">The list of <see cref="TableRow"/> objects to process.</param>
    /// <returns>A list of <see cref="TableRow"/> objects with updated <see cref="TableRow.Name"/> values.</returns>
    /// <remarks>
    /// This method uses a regular expression to remove all occurrences of square brackets ('[' and ']') 
    /// from the <see cref="TableRow.Name"/> property of each <see cref="TableRow"/> in the provided list.
    /// </remarks>
    public static List<TableRow> RemoveBrackets(this List<TableRow> sender)
    {
        sender.ForEach(tr => tr.Name = BracketsAndApostrophesRegex().Replace(tr.Name, ""));
        return sender;
    }

    [GeneratedRegex(@"[\[\]']+")]
    private static partial Regex BracketsAndApostrophesRegex();
    
}

