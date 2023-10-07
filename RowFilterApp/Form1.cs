using RowFilterApp.Classes;

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
        NorthBindingSource.DataSource = await DataOperations.Filtered();
        dataGridView1.DataSource = NorthBindingSource;
        coreBindingNavigator1.BindingSource = NorthBindingSource;
        textBoxSpecial1.BindingSource = NorthBindingSource;
    }
}
