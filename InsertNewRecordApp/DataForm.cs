using System.ComponentModel;
using InsertNewRecordApp.Classes;
using InsertNewRecordApp.Extensions;
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
        dataGridView1.Spread();


        coreBindingNavigator1.BindingSource = _bindingSource;
    }

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        await DataOperations.ResetDapper();

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

    private async void MockUpdateCurrentButton_Click(object sender, EventArgs e)
    {
        Person person = _personList[_bindingSource.Position];

        if (person.Id > 0)
        {
            Person bogus = BogusOperations.Person();
            person.LastName = bogus!.LastName;
            person.FirstName = bogus.FirstName;
            person.BirthDate = bogus.BirthDate;
            await DataOperations.Update(person);
        }
        else
        {
            MessageBox.Show(@"Use Save button than try again");
        }
    }
}
