using DapperSqlServerRowCountSample.Models;
using System.Diagnostics;

namespace DapperSqlServerRowCountSample.Classes
{
    public static class Extensions
    {
        /// <summary>
        /// Determines whether all tables in the provided list have at least one record.
        /// </summary>
        /// <param name="list">The list of <see cref="TableInfo"/> objects representing the tables to check.</param>
        /// <returns>
        /// <c>true</c> if all tables in the list have a row count greater than zero; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool AllTablesHaveRecords(this List<TableInfo> list) 
        => list.All(x => x.RowCount > 0);

        /// <summary>
        /// Converts a boolean value to its corresponding "Yes" or "No" string representation.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <returns>
        /// A string "Yes" if the value is <c>true</c>; otherwise, "No".
        /// </returns>
        [DebuggerStepThrough]
        public static string ToYesNo(this bool value) => value ? "Yes" : "No";
    }
}
