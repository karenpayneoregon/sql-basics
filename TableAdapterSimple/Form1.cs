using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using TableAdapterSimple.BooksDataSetTableAdapters;

namespace TableAdapterSimple
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void booksBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                booksBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(this.booksDataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed\n{ex.Message}");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            categoriesTableAdapter.Fill(booksDataSet.Categories);
            booksTableAdapter.Fill(booksDataSet.Books);
            booksBindingSource.ListChanged += BooksBindingSource_ListChanged;
        }

        /// <summary>
        /// Here we can inspect changes and can reject changes dependent
        /// on validation rules.
        /// </summary>
        private void BooksBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null)
            {
                var book = ((DataRowView)booksBindingSource[e.NewIndex]).Row as BooksDataSet.BooksRow;
            }
        }

        private void GetChangesButton_Click(object sender, EventArgs e)
        {
            /*
             * If sitting on a new or modified row, first move off the row.
             */
            var bookDataTable = booksDataSet.Tables["Books"];
            var modified = bookDataTable.GetChanges(DataRowState.Modified);
            if (modified != null)
            {
                // have some modified rows
            }
            var added = bookDataTable.GetChanges(DataRowState.Added);
            if (added != null)
            {
                // have some added rows
            }
        }

        private void GetConnectionButton_Click(object sender, EventArgs e)
        {
            var result = categoriesTableAdapter.Connection.ConnectionString;
        }
    }
}
