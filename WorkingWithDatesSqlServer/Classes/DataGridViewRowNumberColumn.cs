using System.ComponentModel;

namespace WorkingWithDatesSqlServer.Classes;
public class DataGridViewRowNumberColumn : DataGridViewColumn
{
    public DataGridViewRowNumberColumn() : base()
    {
        this.CellTemplate = new DataGridViewRowNumberCell();
        this.Width = 40;
        this.SortMode = DataGridViewColumnSortMode.NotSortable;
    }
    [Browsable(false)]
    [DefaultValue(true)]
    public override bool ReadOnly
    {
        get { return true; }
        set { base.ReadOnly = true; }
    }
}
public class DataGridViewRowNumberCell : DataGridViewTextBoxCell
{
    protected override object GetValue(int rowIndex)
    {
        return rowIndex + 1;
    }
    protected override bool SetValue(int rowIndex, object value)
    {
        return base.SetValue(rowIndex, rowIndex + 1);
    }
}
