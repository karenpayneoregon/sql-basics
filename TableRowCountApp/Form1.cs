using TableRowCountApp.Classes;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Models;
namespace TableRowCountApp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Shown += Form1_Shown;
        TablesDataGridView.AutoGenerateColumns = false;
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        DatabaseNamesListBox.DataSource = DataOperations.DatabaseNames();
        DatabaseNamesListBox.SelectedIndexChanged += DatabaseNamesListBox_SelectedIndexChanged;
        ShowDetails();
    }

    private void DatabaseNamesListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ShowDetails();
    }

    /// <summary>
    /// Display table details
    /// </summary>
    /// <remarks>
    /// If using <see cref="SqlStatements.DatabaseTablesRowCount"/> to remove brackets we need to use <see cref="Extensions.RemoveBrackets"/>
    /// while the current code using <see cref="SqlStatements.DatabaseTablesRowCount1"/> does not require the use of <see cref="Extensions.RemoveBrackets"/>
    /// because there is a schema and table column and there is a property in the <see cref="TableRow"/> class that matches the column name.
    /// </remarks>
    private void ShowDetails()
    {
        /*
         * Note that using 
         */

        // note using <SqlStatements.DatabaseTablesRowCountSqlStatements.DatabaseTablesRowCount
        TablesDataGridView.DataSource = DataOperations.TableRowCount(DatabaseNamesListBox.Text); //.RemoveBrackets();
        TablesDataGridView.ExpandColumns();
    }
}
