using System.Text.RegularExpressions;

namespace DapperSqlServerLastValueApp.Classes;

/// <summary>
/// Methods to move current row up/down
/// </summary>
public static class DataGridViewExtensions
{
    /// <summary>
    /// Set each column width to the widest data in a column
    /// </summary>
    /// <param name="source"></param>
    /// <param name="sizable"></param>
    public static void ExpandColumns(this DataGridView source, bool sizable = true)
    {
        foreach (DataGridViewColumn col in source.Columns)
        {
            if (col.ValueType.Name != "ICollection`1")
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        if (!sizable) return;

        for (int index = 0; index <= source.Columns.Count - 1; index++)
        {
            int columnWidth = source.Columns[index].Width;

            source.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // Set Width to calculated AutoSize value:
            source.Columns[index].Width = columnWidth;
        }
    }
    /// <summary>
    /// Split on upper-cased letters
    /// </summary>
    /// <param name="source"></param>
    public static void FixHeaders(this DataGridView source)
    {
        string SplitCamelCase(string sender)
            => string.Join(" ", Regex.Matches(sender,
                @"([A-Z][a-z]+)").Select(m => m.Value));

        for (int index = 0; index < source.Columns.Count; index++)
        {
            source.Columns[index].HeaderText = SplitCamelCase(source.Columns[index].HeaderText);
        }
    }
    /// <summary>
    /// Disable all columns sorting
    /// </summary>
    /// <param name="source"></param>
    public static void DisableSorting(this DataGridView source)
    {
        source.Columns
            .Cast<DataGridViewColumn>()
            .ToList()
            .ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
    }
    public static void EnableSorting(this DataGridView source)
    {
        source.Columns
            .Cast<DataGridViewColumn>()
            .ToList()
            .ForEach(f => f.SortMode = DataGridViewColumnSortMode.Automatic);
    }
}