using Dapper;
using System.Data.SqlClient;
using DapperSqlServerLastValueApp.Classes;

namespace DapperSqlServerLastValueApp;

public partial class Form1 : Form
{
    private BindingSource _bindingSource = new ();
    public Form1()
    {
        InitializeComponent();
    }

    private void LoadButton_Click(object sender, EventArgs e)
    {
        _bindingSource.DataSource = DataOperations.List();
        dataGridView1.DataSource = _bindingSource;
        dataGridView1.ExpandColumns();
    }
}
// belong in its own file
public class DataOperations
{
    public static List<ProductItem> List()
    {
        using var cn = new SqlConnection(ConnectionString());
        return cn.Query<ProductItem>(SqlStatements.SelectStatement).AsList();
    }
}
