namespace SqlServerTableRulesApp.Extensions;
public static class DataGridViewExtensions
{
    /// <summary>
    /// Adjusts the column widths of the specified <see cref="DataGridView"/> to fit their content.
    /// </summary>
    /// <param name="source">The <see cref="DataGridView"/> whose columns are to be adjusted.</param>
    /// <param name="sizable">
    /// If <see langword="true"/>, the columns will be resized to fit their content and then set to a fixed width.
    /// If <see langword="false"/>, the columns will remain auto-sized to fit their content.
    /// </param>
    /// <remarks>
    /// Columns containing collections (<see cref="ICollection{T}"/>) are excluded from resizing.
    /// </remarks>
    public static void ExpandColumns(this DataGridView source, bool sizable = false)
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
            source.Columns[index].Width = columnWidth;
        }


    }

}
