using System.Text.RegularExpressions;

namespace CategoriesApplication1.Classes;

public static partial class StringExtensions
{
    /// <summary>
    /// Splits a camel case string into separate words.
    /// </summary>
    /// <param name="sender">The camel case string to be split.</param>
    /// <returns>A string with the words separated by spaces.</returns>
    public static string SplitCase(this string sender)
        => string.Join(" ", CamelCaseRegex().Matches(sender).Select(m => m.Value));

    [GeneratedRegex(@"([A-Z][a-z]+)")]
    private static partial Regex CamelCaseRegex();
}
