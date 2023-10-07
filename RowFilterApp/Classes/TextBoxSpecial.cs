using System.ComponentModel;
using RowFilterApp.Extensions;

#pragma warning disable CS8618

namespace RowFilterApp.Classes;

/// <summary>
/// Goal, to show how to encompass an incremental filter against a BindingSource without code
/// by the caller.
///
/// Also suppresses beep for ENTER key
/// 
/// </summary>
public class TextBoxSpecial : TextBox
{
    /// <summary>
    /// BindingSource with a DataSource set to a DataTable
    /// </summary>
    [Category("Special"), Description("DataSource to filter against")]
    public BindingSource BindingSource { get; set; }

    /// <summary>
    /// Name of the column to filter on
    /// </summary>
    [Category("Special"), Description("Column to filter against")]
    public string ColumnName { get; set; }

    /// <summary>
    /// Should the filter be case sensitive or not
    /// </summary>
    [Category("Special"), Description("Sensitivty for like condition")]
    public bool CaseSensitiveLike { get; set; }

    [Category("Special"), Description("Stash")]
    public string Stash { get; set; }

    /// <summary>
    /// Is there data in the BindingSource
    /// </summary>
    [Browsable(false)]
    public bool HasData { get; set; }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        PerformWork(e);
        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        PerformWork(e);
        base.OnKeyUp(e);
    }

    private void PerformWork(KeyEventArgs e)
    {
        PerformFilter();
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            TriggerEvent?.Invoke();
        }
    }


    private void PerformFilter()
    {
        if (!string.IsNullOrWhiteSpace(Text))
        {
            BindingSource.RowFilterContains(ColumnName, Text, false);
        }
        else
        {
            BindingSource.RowFilterClear();
        }

        HasData = BindingSource.HasData();

    }

    public delegate void TriggerDelegate();
    /// <summary>
    /// Subscribe to be notified when ENTER was pressed.
    /// </summary>
    public event TriggerDelegate TriggerEvent;
}
