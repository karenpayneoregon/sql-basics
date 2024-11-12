using System.Text.RegularExpressions;

namespace BirthdaysFromDatabase.Classes;

public static class DataGridViewExtensions
{
    /// <summary>
    /// Expands the columns of the DataGridView.
    /// </summary>
    /// <param name="source">The DataGridView instance.</param>
    /// <param name="sizable">Determines whether the columns should be sizable.</param>
    public static void ExpandColumns(this DataGridView source, bool sizable = false)
    {
        source.FixHeaders();

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
            source.Columns[index].Width = columnWidth;
        }
    }

    /// <summary>
    /// Fixes the headers of the DataGridView columns to split property names.
    /// </summary>
    /// <param name="source">The DataGridView instance.</param>
    public static void FixHeaders(this DataGridView source)
    {
        for (var index = 0; index < source.Columns.Count; index++)
        {
            source.Columns[index].HeaderText = source.Columns[index].HeaderText.SplitCase();
        }
    }
}
