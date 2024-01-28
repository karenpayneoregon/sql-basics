using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using DapperSimpleApp.Classes;
using DapperSimpleApp.Models;
using DapperSimpleApp.Validators;
using FluentValidation.Results;

namespace DapperSimpleApp
{
    /// <summary>
    /// Zero data operations in this form, just one call to test the connection which
    /// was left for testing for those who are working with this from the DEV article.
    /// </summary>
    public partial class Form1Updated : Form
    {

        private BindingSource _bindingSource = new BindingSource();

        /// <summary>
        /// Provides ability to sort the DataGridView
        /// </summary>
        private SortableBindingList<Person> _bindingList;

        public Form1Updated()
        {
            InitializeComponent();

            bindingNavigatorDeleteItem.Enabled = false;
            dataGridView1.AutoGenerateColumns = false;

            if (Environment.UserName == "PayneK")
            {
                GetAllButton.Enabled = true;
                GetAllPeople();
            }

        }
        private void GetAllPeople()
        {
            // read from database
            var operations = new PersonOperations();
            var list = operations.GetList();

            _bindingList = new SortableBindingList<Person>(list);
            _bindingSource.DataSource = _bindingList;
            bindingNavigator1.BindingSource = _bindingSource;
            dataGridView1.DataSource = _bindingSource;

            _bindingSource.ListChanged += BindingSource_ListChanged;

            CurrentButton.Enabled = true;
            bindingNavigatorDeleteItem.Enabled = true;

            /*
             * Override default action of the BindingNavigator delete button
             * Note step 1 was to remove the default in the designer.
             */
            bindingNavigatorDeleteItem.Click += BindingNavigatorDeleteItem_Click;

            // No records yet so disable delete button in the BindingNavigator
            bindingNavigatorDeleteItem.Enabled = _bindingList.Count > 0;
            dataGridView1.ExpandColumns1();
            
        }

        /// <summary>
        /// Started code for updating a changed record if valid via <see cref="PersonValidator"/>
        /// </summary>
        private void BindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                Person currentPerson = _bindingList[e.OldIndex];
                PersonValidator validator = new PersonValidator();
                ValidationResult result = validator.Validate(currentPerson);

                if (result.IsValid)
                {
                    var operations = new PersonOperations();
                    operations.Update(currentPerson);
                }
                else
                {
                    /*
                     * Reset from database table and in a multi-user environment there may have
                     * been changes to this record since the app started so be aware of this.
                     */
                    var operations = new PersonOperations();
                    var person = operations.Get(currentPerson.Id);
                    _bindingList[e.OldIndex] = person;

                }

            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                // this is after a delete operation 
            }

        }

        /// <summary>
        /// Manually handle delete action
        /// - Ask permission to remove current record
        /// - Delete from database
        /// - If delete successful remove from the BindingList which in turn removes the person from the DataGridView
        /// </summary>
        private void BindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current != null)
            {
                var currentPerson = _bindingList[_bindingSource.Position];
                if (Dialogs.Question($"Delete {currentPerson.FirstName} {currentPerson.LastName} ?"))
                {
                    var operations = new PersonOperations();
                    if (operations.Delete(currentPerson.Id))
                    {
                        _bindingSource.RemoveCurrent();
                        bindingNavigatorDeleteItem.Enabled = _bindingList.Count > 0;
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove record");
                    }
                }
            }
        }

        private void CurrentButton_Click(object sender, EventArgs e)
        {
            var currentPerson = _bindingList[_bindingSource.Position];
            var operations = new PersonOperations();
            var person = operations.Get(currentPerson.Id);
            MessageBox.Show($"From database {person}");
        }

        private void GetAllButton_Click(object sender, EventArgs e)
        {
            GetAllPeople();
        }

        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(ConfigureSettings.ConnectionString()))
            {
                try
                {
                    cn.Open();
                    GetAllButton.Enabled = true;
                    MessageBox.Show("Open");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed: " + ex.Message);
                }

            }
        }

        private void AddNewPersonButton_Click(object sender, EventArgs e)
        {
            using (var f = new AddPersonForm())
            {
                f.ValidPerson += ValidPersonFromChildForm;
                f.ShowDialog();
            }
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
            if (CurrentButton.Enabled)
            {
                var operations = new PersonOperations();
                operations.Add(person);
                _bindingList.Add(person);
            }
            else
            {
                MessageBox.Show("Read all records and try again.");
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (Dialogs.Question("Do you really want to restore person table?"))
            {
                var operations = new PersonOperations();
                operations.ResetData();
                GetAllPeople();
            }
        }
    }
}
