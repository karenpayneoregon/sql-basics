using System.Text.RegularExpressions;

namespace DapperWithExcel.Classes;
internal static class StringExtensions
{
    public static string SplitCamelCase(this string sender) =>
        string.Join(" ", Regex.Matches(sender, @"([A-Z][a-z]+)")
            .Select(m => m.Value));
}
