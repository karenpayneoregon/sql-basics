using Dapper;
using System.Data.SqlClient;
using DapperSqlServerLastValueApp.Classes;

namespace DapperSqlServerLastValueApp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void LoadButton_Click(object sender, EventArgs e)
    {
        dataGridView1.DataSource = DataOperations.List();
        dataGridView1.ExpandColumns();
    }


}

public class DataOperations
{
    public static List<ProductItem> List()
    {
        using var cn = new SqlConnection(ConnectionString());
        return cn.Query<ProductItem>(SqlStatements.SelectStatement).AsList();
    }
}
