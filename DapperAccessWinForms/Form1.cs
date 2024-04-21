using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using Dapper;

namespace DapperAccessWinForms;
#nullable disable

/*
 * For the dropdown, set DropDownStyle to DropDownList
 */
public partial class Form1 : Form
{
    private BindingList<Category> _bindingCategories;
    public Form1()
    {
        InitializeComponent();
        Shown += Form1_Shown;
    }

    private async void Form1_Shown(object sender, EventArgs e)
    {
        DataOperations dataOperations = new();
        
        _bindingCategories = new BindingList<Category>(
            await dataOperations.GetCategories());

        CategoriesComboBox.DataSource = _bindingCategories;
    }

    private void GetCategoryButton_Click(object sender, EventArgs e)
    {
        Category category = _bindingCategories[CategoriesComboBox.SelectedIndex];
        MessageBox.Show($"{category.Identifier} {category.CategoryName}");
    }
}

// place in its own file
public class Category
{
    public int Identifier { get; set; }
    public string CategoryName { get; set; }
    public override string ToString() => CategoryName;
}
// place in its own file
public class DataOperations
{
    private IDbConnection _cn = new OleDbConnection(ConnectionString());
    public async Task<List<Category>> GetCategories()
        => (await _cn.QueryAsync<Category>(
            "SELECT Identifier, CategoryName FROM Categories")).AsList();
}

