using System.Diagnostics;

namespace EntityFrameworkCoreSqlServer.Classes;

public static class Extensions
{
    [DebuggerStepThrough]
    public static string ToYesNo(this bool value) => value ? "Yes" : "No";
}