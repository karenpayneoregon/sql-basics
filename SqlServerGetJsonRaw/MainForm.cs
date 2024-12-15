using SqlServerGetJsonRaw.Classes;

namespace SqlServerGetJsonRaw;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        DapperOperations clientOperations = new();
        var result = clientOperations.PersonData2("Payne");


        PersonDataGridView.AutoGenerateColumns = false;

        DapperOperations operations = new();
        LastNamesComboBox.DataSource = operations.LastNames();

        PopulateDataControls(operations);

        LastNamesComboBox.SelectedIndexChanged += (sender, e) 
            => PopulateDataControls(operations);

    }

    /// <summary>
    /// Populates the data controls on the form with person and address data
    /// based on the currently selected last name in the <see cref="LastNamesComboBox"/>.
    /// </summary>
    /// <param name="operations">
    /// An instance of <see cref="DapperOperations"/> used to retrieve person and address data.
    /// </param>
    private void PopulateDataControls(DapperOperations operations)
    {
        
        var (person, addresses) = 
            operations.PersonData1(LastNamesComboBox.Text);

        PersonDataGridView.DataSource = person;
        AddressesDataGridView.DataSource = addresses;
    }

}