using RowFilterApp.Classes;
using RowFilterApp.Extensions;

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

        NorthBindingSource.DataSource = await DataOperations.ReadCustomersData();
        dataGridView1.DataSource = NorthBindingSource;

        dataGridView1.ExpandColumns();

        coreBindingNavigator1.BindingSource = NorthBindingSource;
        containsTextBox.BindingSource = NorthBindingSource;

    }
}
