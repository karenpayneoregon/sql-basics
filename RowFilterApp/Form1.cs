using RowFilterApp.Classes;
using RowFilterApp.Extensions;
using System.Windows.Forms;
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace RowFilterApp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        Shown += Form1_Shown;
        NorthBindingSource = new BindingSource();
    }

    public BindingSource NorthBindingSource { get; set; }

    private async void Form1_Shown(object? sender, EventArgs e)
    {

        dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;
        NorthBindingSource.DataSource = await DataOperations.ReadCustomersData();
        dataGridView1.DataSource = NorthBindingSource;

        dataGridView1.ExpandColumns();

        coreBindingNavigator1.BindingSource = NorthBindingSource;
        containsTextBox.BindingSource = NorthBindingSource;

    }

    private void DataGridView1_RowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
    {
        var dgv = sender as DataGridView;
        var rowIndex = (e.RowIndex + 1).ToString();

        var centerFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height);
        e.Graphics.DrawString(rowIndex, Font, SystemBrushes.ControlText, headerBounds, centerFormat);
    }
}
