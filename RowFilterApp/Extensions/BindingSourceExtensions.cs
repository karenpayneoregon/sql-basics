using System.Data;

namespace RowFilterApp.Extensions;
public static class BindingSourceExtensions
{
    /// <summary>
    /// Cast DataSource to a DataTable
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static DataTable DataTable(this BindingSource sender) => (DataTable)sender.DataSource;

    /// <summary>
    /// Access the DataView of the DataTable
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static DataView DataView(this BindingSource sender) => ((DataTable)sender.DataSource).DefaultView;
    /// <summary>
    /// Check for DataSource is not null
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static bool HasData(this BindingSource sender) => sender.DataSource != null;

    /// <summary>
    /// Apply a filter for Like 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="field">Field to apply filter on</param>
    /// <param name="value">Value for filter</param>
    /// <param name="caseSensitive">Filter should be case or case in-sensitive</param>
    public static void RowFilterContains(this BindingSource sender, string field, string value, bool caseSensitive = false)
    {
        sender.DataTable().CaseSensitive = caseSensitive;
        sender.DataView().RowFilter = $"{field} LIKE '%{value.EscapeApostrophe()}%'";
    }
    /// <summary>
    /// Apply a filter for Like starts with
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="field">Field to apply filter on</param>
    /// <param name="value">Value for filter</param>
    /// <param name="caseSensitive">Filter should be case or case in-sensitive</param>
    public static void RowFilterStartsWith(this BindingSource sender, string field, string value, bool caseSensitive = false)
    {
        sender.DataTable().CaseSensitive = caseSensitive;
        sender.DataView().RowFilter = $"{field} LIKE '{value.EscapeApostrophe()}%'";
    }
    public static void RowFilterEndsWith(this BindingSource sender, string field, string value, bool caseSensitive = false)
    {
        sender.DataTable().CaseSensitive = caseSensitive;
        sender.DataView().RowFilter = $"{field} LIKE '%{value.EscapeApostrophe()}'";
    }

    /// <summary>
    /// Clear DataView RowFilter
    /// </summary>
    /// <param name="sender"></param>
    public static void RowFilterClear(this BindingSource sender)
    {
        sender.DataView().RowFilter = "";
    }

}