using SqlServer.Library.Classes;

namespace DapperGetDatabaseAndTableNamesApp1;
public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();

        Shown += Form2_Shown;
    }

    private async void Form2_Shown(object? sender, EventArgs e)
    {
        // get all database names
        DatabaseNamesComboBox.DataSource = await DataOperations.DatabaseNamesAsync();
        // setup for getting table names from selected database
        DatabaseNamesComboBox.SelectedIndexChanged += DatabaseNamesComboBox_SelectedIndexChanged;
        // get all table names for the selected database (in this case the first in the combo box)
        await GetTablesForDatabase();
    }

    private async void DatabaseNamesComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        await GetTablesForDatabase();
    }

    private async Task GetTablesForDatabase()
    {
        TableNamesComboBox.DataSource = await DataOperations.TableNamesAsync(DatabaseNamesComboBox.Text);
    }
}
