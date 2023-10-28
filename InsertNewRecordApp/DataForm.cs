using System.ComponentModel;
using InsertNewRecordApp.Classes;
using InsertNewRecordApp.MockingClasses;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp;
public partial class DataForm : Form
{
    private BindingList<Person> _personList;
    private BindingSource _bindingSource = new();
    public DataForm()
    {
        InitializeComponent();

        _personList = new BindingList<Person>(BogusOperations.People());
        _bindingSource.DataSource = _personList;
        dataGridView1.DataSource = _bindingSource;
        coreBindingNavigator1.BindingSource = _bindingSource;
    }

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        await DataOperations.Reset();

        var (success, exception) = await DataOperations.AddRangeDapperWithoutKeys(_personList.ToList());
        if (exception is not null)
        {
            MessageBox.Show($@"Failed with {exception.Message}");
        }
        else
        {
            _personList = new BindingList<Person>(DataOperations.GetAll());
            _bindingSource.DataSource = _personList;
        }
    }

    private void CurrentButton_Click(object sender, EventArgs e)
    {
        Person person = _personList[_bindingSource.Position];

        MessageBox.Show($@"{person.Id} {person.FirstName} {person.LastName} {person.BirthDate}");
    }
}
