namespace RowFilterApp.Extensions;

public static class StringExtensions
{
    public static string EscapeApostrophe(this string pSender) => pSender.Replace("'", "''");
}