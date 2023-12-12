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
        using var cn = new SqlConnection(ConnectionString());
        dataGridView1.DataSource = cn.Query<ProductItem>(SqlStatements.SelectStatement);
        dataGridView1.ExpandColumns();
    }
}
