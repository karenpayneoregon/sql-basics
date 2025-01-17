using DapperGetDatabaseAndTableNamesApp1.Classes;
using Serilog;
using SqlServer.Library.Classes;

namespace DapperGetDatabaseAndTableNamesApp1;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        Shown += Form1_Shown;
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        DatabaseNamesComboBox.DataSource = DataOperations.DatabaseNames();

        int position = DatabaseNamesComboBox.FindString("NorthWind2024");

        if (position > -1)
        {
            DatabaseNamesComboBox.SelectedIndex = position;
        }
    }

    private async void IterateDatabasesButton_Click(object sender, EventArgs e)
    {
        try
        {
            IterateDatabasesButton.Enabled = false;
            ResultsTextBox.Text = await Operations.IterateDatabases();
            IterateDatabasesButton.Enabled = true;
        }
        catch (Exception exception)
        {
            Log.Error(exception,nameof(IterateDatabasesButton_Click));
        }
    }

    private void ForeignKeysButton_Click(object sender, EventArgs e)
    {
        ResultsTextBox.Text = Operations.GetForeignKeys(DatabaseNamesComboBox.Text);
    }
}
