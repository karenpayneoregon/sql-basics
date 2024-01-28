using System;
using System.Globalization;
using System.Windows.Forms;

namespace DapperSimpleApp.Classes
{
    /// <summary>
    /// Provides a calendar column for the DataGridView control.
    /// Original code from Microsoft https://tinyurl.com/y8nr9okh
    /// </summary>
    public class CalendarColumn : DataGridViewColumn
    {
        public CalendarColumn() : base(new CalendarCell())
        {
        }
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;
            set
            {

                if (value != null && !(value.GetType().IsAssignableFrom(typeof(CalendarCell))))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }

                base.CellTemplate = value;
            }
        }
    }
    public class CalendarCell : DataGridViewTextBoxCell
    {
        public CalendarCell()
        {
            Style.Format = "d"; // Use the short date format.
            EmptyDate = DateTime.Now;
        }
        /// <summary>
        /// Set default Date
        /// </summary>
        public DateTime EmptyDate { get; set; }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            var theControl = (CalendarEditingControl)DataGridView.EditingControl;

            if (Convert.IsDBNull(Value) || (Value == null))
            {
                theControl.Value = DateTime.Now;
            }
            else
            {
                theControl.Value = Convert.ToDateTime(Value);
            }

        }
        public override Type EditType => typeof(CalendarEditingControl);
        public override Type ValueType => typeof(DateTime);
        public override object DefaultNewRowValue => DateTime.Now;
    }
    /// <summary>
    /// Provides Calendar popup within the DataGridView.
    /// </summary>
    /// <remarks></remarks>
    internal class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView _dataGridViewControl;
        private bool _valueChanged = false;
        private int _rowIndexNumber;

        public CalendarEditingControl()
        {
            Format = DateTimePickerFormat.Short;
        }

        public object EditingControlFormattedValue
        {
            get => Value.ToShortDateString();
            set
            {
                if (value is string)
                {
                    Value = DateTime.Parse(Convert.ToString(value));
                }
            }
        }
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            CalendarForeColor = dataGridViewCellStyle.ForeColor;
            CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        /// <inheritdoc />
        public int EditingControlRowIndex
        {
            get => _rowIndexNumber;
            set => _rowIndexNumber = value;
        }
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            if (((key & Keys.KeyCode) == Keys.Left) ||
                ((key & Keys.KeyCode) == Keys.Up) ||
                ((key & Keys.KeyCode) == Keys.Down) ||
                ((key & Keys.KeyCode) == Keys.Right) ||
                ((key & Keys.KeyCode) == Keys.Home) ||
                ((key & Keys.KeyCode) == Keys.End) ||
                ((key & Keys.KeyCode) == Keys.PageDown) ||
                ((key & Keys.KeyCode) == Keys.PageUp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        public bool RepositionEditingControlOnValueChange => false;

        public DataGridView EditingControlDataGridView
        {
            get => _dataGridViewControl;
            set => _dataGridViewControl = value;
        }
        public bool EditingControlValueChanged
        {
            get => _valueChanged;
            set => _valueChanged = value;
        }
        Cursor IDataGridViewEditingControl.EditingPanelCursor => EditingControlCursor;
        public Cursor EditingControlCursor => base.Cursor;
        protected override void OnValueChanged(EventArgs eventArgs)
        {
            _valueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventArgs);
        }
    }
}
