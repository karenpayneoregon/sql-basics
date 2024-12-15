using SqlServerGetJsonRaw.Models;
using System.ComponentModel;
using SqlServerGetJsonRaw.Classes;

#pragma warning disable CS8618, CS9264

namespace SqlServerGetJsonRaw;
public partial class SqlClientForm : Form
{
    private BindingList<PersonSqlClient> _bindingList;
    private BindingSource _bindingSource = new();
    private SqlClientOperations _sqlClientOperations = new();
    public SqlClientForm()
    {
        InitializeComponent();

        Shown += SqlClientForm_Shown;
    }

    private void SqlClientForm_Shown(object? sender, EventArgs e)
    {
        PersonDataGridView.AutoGenerateColumns = false;

        LastNamesComboBox.DataSource = _sqlClientOperations.LastNames();
        LastNamesComboBox.SelectedIndexChanged += (_, _) => PopulateDataControls();

        PopulateDataControls();
    }

    private void PopulateDataControls()
    {

        _bindingList = new BindingList<PersonSqlClient>(_sqlClientOperations.GetPerson(LastNamesComboBox.Text));
        _bindingSource.DataSource = _bindingList;

        PersonDataGridView.DataSource = _bindingSource;
        AddressesDataGridView.DataSource = _bindingList.FirstOrDefault()!.Addresses;

    }
}
