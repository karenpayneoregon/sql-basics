using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    protected override void Paint(System.Drawing.Graphics graphics,
        System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds,
        int rowIndex, DataGridViewElementStates cellState, object value,
        object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
    {
        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value,
            formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
    }
    protected override object GetValue(int rowIndex)
    {
        return rowIndex + 1;
    }
    protected override bool SetValue(int rowIndex, object value)
    {
        return base.SetValue(rowIndex, rowIndex + 1);
    }
}
