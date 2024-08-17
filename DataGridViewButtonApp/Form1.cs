using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using DataGridViewButtonApp.Controls;
using DataGridViewButtonApp.Extensions;
using static System.Windows.Forms.DataFormats;
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

namespace DataGridViewButtonApp;

public partial class Form1 : Form
{
    public BindingSource BindingSource { get; set; }
    /// <summary>
    /// Name of the remove DataGridViewButton
    /// </summary>
    public string RemoveButtonName { get; set; }
    /// <summary>
    /// Name of the edit DataGridViewButton
    /// </summary>
    public string EditButtonName { get; set; } = "EditColumn";

    public string _removeButtonName = "RemoveColumn";
    private string editButtonName = "EditColumn";
    private int mIdentifier = 0;

    public Form1()
    {
        InitializeComponent();
        BindingSource = new BindingSource { DataSource = SetupDataSource() };

        BindingSource.PositionChanged += bsCustomers_PositionChanged;
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.DataSource = BindingSource;
        dataGridView1.CreateUnboundButtonColumn(editButtonName, "Edit", "");
        dataGridView1.CellEnter += CellEnter;
        dataGridView1.CellLeave += CellLeave;
        dataGridView1.CellContentClick += CellContentClick;
        dataGridView1.Sorted += DataGridView1_Sorted;

        dataGridView1.AdjustButtons(editButtonName);

        SetReadOnlyColumns();

        ActiveControl = dataGridView1;
        Shown += Form1_Shown;
  
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        BindingSource.MoveNext();
        BindingSource.MoveFirst();
    }

    public void SetReadOnlyColumns()
    {
        foreach (DataGridViewColumn column in dataGridView1.Columns)
        {
            if (!column.IsA<DataGridViewButtonColumn>())
            {
                column.ReadOnly = true;
            }
        }
    }

    public DataTable SetupDataSource()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn()
        {
            ColumnName = "Id", 
            DataType = typeof(int), 
            AutoIncrement = true, 
            ColumnMapping = MappingType.Hidden
        } );

        dt.Columns.Add(new DataColumn() {ColumnName = "FirstName", DataType = typeof(string)} );
        dt.Columns.Add(new DataColumn() {ColumnName = "LastName", DataType = typeof(string)} );
        dt.Rows.Add(null, "Karen", "Payne");
        dt.Rows.Add(null, "Billy", "Jones");

        return dt;
    }

    private void bsCustomers_PositionChanged(object sender, EventArgs e)
    {
        if (BindingSource.Current != null)
        {
            mIdentifier = BindingSource.CurrentRow().Field<int>("Id");
        }
    }

    private void DataGridView1_Sorted(object sender, EventArgs e)
    {
        BindingSource.Position = BindingSource.Find("Id", mIdentifier);
        dataGridView1.AdjustButtons(editButtonName);
    }

    /// <summary>
    /// Used for performing an action on the current row of the DataGridView
    /// </summary>
    public void CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (!dataGridView1.IsHeaderButtonCell(e)) return;

        var columnName = dataGridView1.Columns[e.ColumnIndex].Name;
        if (columnName != EditButtonName) return;

        DataRow row = BindingSource.CurrentRow();
        if (row.Field<string>("LastName") == "Payne")
        {
            Process.Start((ProcessStartInfo)new()
            {
                UseShellExecute = true,
                FileName = "https://github.com/karenpayneoregon?tab=repositories",
            });
        }
        else
        {
            Process.Start((ProcessStartInfo)new()
            {
                UseShellExecute = true,
                FileName = "https://spectreconsole.net/",
            });
        }
    }
    public void CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        var dgv = ((DataGridView)sender);

        DataGridViewDisableButtonCell editButtonCell = 
            (DataGridViewDisableButtonCell)(dgv.Rows[e.RowIndex].Cells[EditButtonName]);

        editButtonCell.Enabled = true;

        if (editButtonCell.Value == null)
        {
            editButtonCell.Value = EditButtonName;
        }

        dgv.Invalidate();
    }
    public void CellLeave(object sender, DataGridViewCellEventArgs e)
    {
        var dgv = ((DataGridView)sender);
        
        DataGridViewDisableButtonCell editButtonCell = 
            (DataGridViewDisableButtonCell)(dgv.Rows[e.RowIndex].Cells[EditButtonName]);

        editButtonCell.Enabled = false;
        dgv.Invalidate();
    }
}