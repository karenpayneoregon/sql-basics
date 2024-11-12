
using BirthdaysFromDatabase.Classes;

namespace BirthdaysFromDatabase;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        dataGridView1.DataSource = new SqlOperations().List();
        dataGridView1.ExpandColumns();
    }
}
