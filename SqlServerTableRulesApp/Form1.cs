using System.ComponentModel;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Models;
using SqlServerTableRulesApp.Extensions;

namespace SqlServerTableRulesApp;

public partial class Form1 : Form
{
    /*
     * These are provided in the event you want to get at
     * the current TableConstraints or all to export or
     * simply get at an item.
     */
    private BindingList<TableConstraints> _bindingList;
    private BindingSource _bindingSource = [];
    public Form1()
    {
        InitializeComponent();

        Shown += Form1_Shown;
    }

    private async void Form1_Shown(object sender, EventArgs e)
    {
        DatabaseService service = new();
        DatabaseNamesComboBox.DataSource = await service.DatabaseNames();
        var position = DatabaseNamesComboBox.FindString("NorthWind2024");
        if (position != -1)
        {
            DatabaseNamesComboBox.SelectedIndex = position;
        }

        GetRulesButton.Enabled = true;
        CurrentButton.Enabled = true;
    }

    private async void GetRulesButton_Click(object sender, EventArgs e)
    {
        TableService service = new();

        _bindingList = new BindingList<TableConstraints>(await service.GetAllTableConstraints(DatabaseNamesComboBox.Text));
        _bindingSource.DataSource = _bindingList;
        dataGridView1.DataSource = _bindingSource;
        dataGridView1.ExpandColumns();
    }

    private void CurrentButton_Click(object sender, EventArgs e)
    {
        if (_bindingList is not null)
        {
            var current = _bindingList[_bindingSource.Position];
        }
    }
}
