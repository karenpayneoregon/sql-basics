using System.ComponentModel;
using InsertNewRecordApp.Classes;
using InsertNewRecordApp.Models;

namespace InsertNewRecordApp;

public partial class Form1 : Form
{
    private BindingList<Person> _personList;
    private BindingSource _bindingSource = new();
    public Form1()
    {
        InitializeComponent();

        // newly added records
        _personList = new BindingList<Person>(DataOperations.AddRange());
        _bindingSource.DataSource = _personList;
        dataGridView1.DataSource = _bindingSource;

        // get total count of records
        CountLabel.Text = DataOperations.PeopleCount().ToString();

    }
    /// <summary>
    /// How to get the current person
    /// </summary>
    private void GetCurrentPersonButton_Click(object sender, EventArgs e)
    {
        Person person = _personList[_bindingSource.Position];

        MessageBox.Show($@"{person.Id} {person.FirstName} {person.LastName} {person.BirthDate}");
    }
}
