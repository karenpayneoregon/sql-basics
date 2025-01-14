using System.ComponentModel;
using System.Text.Json;
using SqlServerGetJsonRaw.Classes;
using SqlServerGetJsonRaw.Classes.Configuration;
using SqlServerGetJsonRaw.Models;
#pragma warning disable CS8618, CS9264

namespace SqlServerGetJsonRaw;
public partial class DapperForm : Form
{
    private BindingList<PersonDapper2> _bindingList;
    private BindingSource _bindingSource = new();
    private DapperOperations _dapperOperations = new();
    public DapperForm()
    {
        InitializeComponent();

        Shown += DapperForm_Shown;

    }

    private void DapperForm_Shown(object? sender, EventArgs e)
    {

        PersonDataGridView.AutoGenerateColumns = false;

        LastNamesComboBox.DataSource = _dapperOperations.LastNames();
        LastNamesComboBox.SelectedIndexChanged += (_, _) => PopulateDataControls();

        PopulateDataControls();
    }

    private void PopulateDataControls()
    {

        _bindingList = new BindingList<PersonDapper2>(_dapperOperations.PersonData2(LastNamesComboBox.Text));
        _bindingSource.DataSource = _bindingList;

        PersonDataGridView.DataSource = _bindingSource;
        AddressesDataGridView.DataSource = _bindingList.FirstOrDefault()!.Addresses;
        AddressesDataGridView.ExpandColumns();

    }

    private void AddMockedPersonButton_Click(object sender, EventArgs e)
    {
        //_dapperOperations.AddPersonMockup();

var str =
    """
    first line;
    also first line
    """.ReplaceLineEndings("");
    }
}
