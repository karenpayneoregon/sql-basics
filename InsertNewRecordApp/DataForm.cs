using System.ComponentModel;
using InsertNewRecordApp.Classes;
using InsertNewRecordApp.Extensions;
using InsertNewRecordApp.MockingClasses;
using InsertNewRecordApp.Models;

using static InsertNewRecordApp.Classes.Dialogs;

namespace InsertNewRecordApp;
public partial class DataForm : Form
{
    private BindingList<Person> _personList;
    private BindingSource _bindingSource = new();
    public DataForm()
    {
        InitializeComponent();

        LoadBogusData();

    }

    private void LoadBogusData()
    {

        _personList = new BindingList<Person>(BogusOperations.People());
        _bindingSource.DataSource = _personList;
        dataGridView1.DataSource = _bindingSource;
        dataGridView1.Spread();


        coreBindingNavigator1.BindingSource = _bindingSource;
    }

    /*
     *   1. Dump current table data, reset identity for table using Dapper
     *   2. Add all rows in the DataGridView to the database table
     *   3. Read back data to DataGridView
     *
     *   Note:
     *   The idea for this example might be done without a user interface so
     *   the data is not returned but the list is updated, check it out by
     *   uncommenting updatedPeople and put a breakpoint below and check that
     *   each item has a primary key.
     */
    private async void SaveButton_Click(object sender, EventArgs e)
    {
        // reset database table
        await DataOperations.ResetDapper();

        // Add range of people to database
        var (success, exception) = await DataOperations.AddRangeDapperWithKeys(_personList.ToList());

        if (exception is not null)
        {
            MessageBox.Show($@"Failed with {exception.Message}");
        }
        else
        {
            // see comments above
            // List<Person> updatedPeople = _personList.ToList();

            _personList = new BindingList<Person>(DataOperations.GetAll());
            _bindingSource.DataSource = _personList;

        }
    }

    /*
     *  1. Get id from current row in the DataGridView
     *  2. Use Dapper to find in the database table
     *     (We could get it right from the BindingList and be done but we are here for Dapper)
     *  3. Display person data
     */
    private async void CurrentButton_Click(object sender, EventArgs e)
    {
        // check if there is a current row in the DataGridView
        if (_bindingSource.Current is null) return;

        // Get current person in the DataGridView
        Person person = _personList[_bindingSource.Position];

        if (person.Id > 0)
        {
            // Get record for current DataGridView row in database using Dapper
            var record = await DataOperations.GetDapper(person.Id);
            MessageBox.Show($@"{record.Id} {record.FirstName} {record.LastName} {record.BirthDate:MM/dd/yyyy}");
        }
        else
        {
            MessageBox.Show(@"Use Save button than try again");
        }

    }
    /// <summary>
    /// Get current person from current row in the DataGridView
    /// </summary>
    private async void MockUpdateCurrentButton_Click(object sender, EventArgs e)
    {
        // check if there is a current row in the DataGridView
        if (_bindingSource.Current is null) return;

        // Get current person in the DataGridView
        Person person = _personList[_bindingSource.Position];

        if (person.Id > 0)
        {
            BogusPerson(person);
            await DataOperations.UpdateDapper(person);
            //await DataOperations.Update(person);
        }
        else
        {
            MessageBox.Show(@"Use Save button than try again");
        }
    }

    /*
     * Remove current record requirements
     *    1. Ensure we have a person to remove
     *    2. Ensure the person has a primary key
     */
    private async void RemoveButton_Click(object sender, EventArgs e)
    {

        // check if there is a current row in the DataGridView
        if (_bindingSource.Current is null) return;

        // Get current person in the DataGridView
        Person person = _personList[_bindingSource.Position];

        if (person.Id > 0)
        {
            /*
             * Although this is mocked data, always wise to ask permission.
             * Note that the Question method will
             *   - Always center on the form
             *   - Default to No (inspect the code, you can set button text as you see fit)
             */
            if (!Question(this, $"Remove {person.FirstName} {person.LastName}", Properties.Resources.question32))
            {
                return;
            }

            if (await DataOperations.RemoveDapper(person))
            {
                //await DataOperations.Remove(person);
                _bindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Failed to remove record");
            }
        }
        else
        {
            MessageBox.Show(@"Use Save button than try again");
        }
    }

    /*
     * AddDapper sets the primary key which means we see it in the grid
     */
    private async void AddButton_Click(object sender, EventArgs e)
    {

        // create a bogus/mocked person using Bogus
        Person person = BogusOperations.Person();

        // Add Person to database
        await DataOperations.AddDapper(person);

        // Add person to the BindingList which will display in the DataGridView
        _personList.Add(person);
    }

    /*
     * Create a mocked/random person
     */
    private static void BogusPerson(Person person)
    {
        Person bogus = BogusOperations.Person();
        person.LastName = bogus!.LastName;
        person.FirstName = bogus.FirstName;
        person.BirthDate = bogus.BirthDate;
    }

    /*
     * Reload DataGridView with another batch of bogus people
     */
    private async void RefreshButton_Click(object sender, EventArgs e)
    {
        await DataOperations.ResetDapper();
        LoadBogusData();
    }

    /*
     * Here we show how to perform a WHERE BETWEEN on BirthDate
     *  - Read people from json file with known data
     *  - Reset database table and populate from json
     *  - Execute where condition and display in the DataGridView
     */
    private async void WhereButton_Click(object sender, EventArgs e)
    {
        var peep = JsonOperations.GetAll();
        await DataOperations.SetupWithJson(peep);
        _personList = new BindingList<Person>(DataOperations.GetBetweenDates(1999, 2004));
        _bindingSource.DataSource = _personList;
    }

    private async void GetCustomerButton_Click(object sender, EventArgs e)
    {
        var customer = await DataOperations.GetContrib(2);
        MessageBox.Show(customer.ToString());
    }

    private async void AddCustomerButton_Click(object sender, EventArgs e)
    {
        /*
         * Basics for Contrib methods
         */
        Customer customer = new Customer() { FirstName = "Karen", LastName = "Payne", Active = true };
        await DataOperations.AddContrib(customer);
        customer.FirstName = "Jane";
        var test1 = await DataOperations.UpdateContrib(customer);
        var test2 = await DataOperations.RemoveContrib(customer);
    }
}
