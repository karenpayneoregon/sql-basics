using System.ComponentModel;
using FluentValidation.Results;
using SimpleEfCoreApp.Classes;
using SimpleEfCoreApp.Models;
using SimpleEfCoreApp.Validators;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace SimpleEfCoreApp;

public partial class Form1 : Form
{

    private BindingSource _bindingSource = new();

    /// <summary>
    /// Provides ability to sort the DataGridView
    /// </summary>
    private SortableBindingList<Person> _bindingList;
    public Form1()
    {
        InitializeComponent();
        dataGridView1.AutoGenerateColumns = false;
        ClientSize = new Size(350, 322);
        Shown += Form1_Shown;
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {

        _bindingList = new SortableBindingList<Person>(PersonOperations.GetAll());
        _bindingSource.DataSource = _bindingList;
        bindingNavigator1.BindingSource = _bindingSource;
        dataGridView1.DataSource = _bindingSource;
        dataGridView1.ExpandColumns();

        _bindingSource.ListChanged += BindingSource_ListChanged!;
        bindingNavigator1.RemoveButton.Click += RemoveButton_Click;
        dataGridView1.DataError += DataGridView1_DataError;

    }

    private void DataGridView1_DataError(object? sender, DataGridViewDataErrorEventArgs e)
    {

        if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == nameof(BirthDateColumn))
        {
            MessageBox.Show("Invalid date");
        }

    }

    /// <summary>
    /// Started code for updating a changed record if valid via <see cref="PersonValidator"/>
    /// </summary>
    private void BindingSource_ListChanged(object sender, ListChangedEventArgs e)
    {
        if (e.ListChangedType == ListChangedType.ItemChanged)
        {
            Person currentPerson = _bindingList[_bindingSource.Position];
            PersonValidator validator = new PersonValidator();
            ValidationResult result = validator.Validate(currentPerson);

            if (result.IsValid)
            {
                PersonOperations.Update(currentPerson);
            }
            else
            {
                /*
                 * Reset from database table and in a multi-user environment there may have
                 * been changes to this record since the app started so be aware of this.
                 */
                var person = PersonOperations.Get(currentPerson.Id);
                _bindingList[_bindingSource.Position] = person!;
            }

        }
        else if (e.ListChangedType == ListChangedType.ItemDeleted)
        {
            // this is after a delete operation 
        }

    }

    private void RemoveButton_Click(object? sender, EventArgs e)
    {
        var currentPerson = _bindingList[_bindingSource.Position];
        if (Dialogs.Question("Question", $"Remove {currentPerson.FirstName} {currentPerson.LastName}", "Yes", "No", DialogResult.No))
        {
            if (!PersonOperations.Delete(currentPerson.Id))
            {
                MessageBox.Show("Delete failed");
            }
            else
            {
                _bindingSource.RemoveCurrent();
            }
        }
    }

    private void ResetDataButton_Click(object sender, EventArgs e)
    {
        PersonOperations.ResetData();
        MessageBox.Show("Data has been reset");
    }

    private void CurrentPersonButton_Click(object sender, EventArgs e)
    {
        if (_bindingSource.Current is not null)
        {
            var currentPerson = _bindingList[_bindingSource.Position];
            MessageBox.Show($"{currentPerson.Id} {currentPerson.FirstName} {currentPerson.LastName} born {currentPerson.BirthDate:d}");
        }
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        using var f = new AddPersonForm();
        f.ValidPerson += ValidPersonFromChildForm;
        f.ShowDialog();
    }
    /// <summary>
    /// If the <see cref="Person"/> is valid in the child form, pass it back to
    /// here which perhaps add to the database table than if successful add to the
    /// BindingList which will have it shown in the DataGridView.
    ///
    /// This happens only if the DataGridView is currently populated.
    /// </summary>
    /// <param name="person"></param>
    private void ValidPersonFromChildForm(Person person)
    {
        if (CurrentPersonButton.Enabled)
        {
            if (PersonOperations.Add(person))
            {
                _bindingList.Add(person);
                _bindingSource.MoveLast();
            }
            else
            {
                MessageBox.Show("Failed adding person");
            }
        }
    }
}
