using Microsoft.Data.SqlClient;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Operations.GetList();
    }
}

public class Operations
{
    public static readonly string ConnectionString = 
        """
        Data Source=.\SQLEXPRESS;Initial Catalog=ForumExample;
        Integrated Security=True;Encrypt=False
        """;

    public static List<SelectListItem> GetList()
    {
        List<SelectListItem> listCollection = new();
        using SqlConnection cn = new(ConnectionString);
        cn.Open();
        using SqlCommand cmd = new() {Connection = cn};

        cmd.CommandText =
            """
            SELECT DISTINCT COALESCE(LastName, 'Is Null') AS LastName
            FROM dbo.DemoNull;
            """;
        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            /*
             * First is nullable, no warning
             * Second not nullable, warning
             */
            listCollection.Add(new SelectListItem(dr[0].ToString(), dr[0].ToString()));
        }

        return listCollection;
    }
}

public class SelectListItem
{
    public SelectListItem(string? value1, string value2)
    {
        if (value1 == "Is Null")
        {
            
        }
        else
        {
            
        }
    }
}
