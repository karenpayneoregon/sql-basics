    

    using SqlServerLibrary.Models;
    using static System.Text.RegularExpressions.Regex;

    namespace TableRowCountApp.Classes;
    public static class Extensions
    {
        public static List<TableRow> RemoveBrackets(this List<TableRow> sender)
        {
            sender.ForEach(tr => tr.Name = Replace(tr.Name, @"[\[\]']+", ""));
            return sender;
        }
    }

