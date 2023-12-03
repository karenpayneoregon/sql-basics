using System.ComponentModel;
using DataGridViewSample.Classes;
using DataGridViewSample.Models;

namespace DataGridViewSample
{
    public partial class Form1 : Form
    {
        private readonly BindingSource _bindingSource = new();
        private BindingList<Book> _bindingList = new();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            _bindingList = DataSource.Instance.BindingList;
            _bindingSource.DataSource = _bindingList;
            dataGridView1.DataSource = _bindingSource;
            dataGridView1.ExpandColumns();
        }

        private async void GetCurrentFromTableButton_Click(object sender, EventArgs e)
        {
            Book book = _bindingList[_bindingSource.Position];
            var book1 = await DataOperations.GetCategory(book.Id, book.CategoryId);
        }

        private void EditCurrentButton_Click(object sender, EventArgs e)
        {
            EditForm form = new EditForm(_bindingSource.Position);
            form.ShowDialog();
        }
    }
}