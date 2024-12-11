
using SqlServerGetJsonRaw.Classes;

namespace SqlServerGetJsonRaw;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        dataGridView1.AllowUserToAddRows = false;

        DataOperations operations = new();
        LastNamesComboBox.DataSource = operations.LastNames();
    }

    private void ReadButton_Click(object sender, EventArgs e)
    {
        DataOperations operations = new();
        var result = operations.GetPerson(LastNamesComboBox.Text);
        dataGridView1.DataSource = result;
    }
}