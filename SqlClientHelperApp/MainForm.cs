
using SqlClientHelperApp.Classes;
using SqlClientHelperApp.Classes.Configuration;
using static SqlServer.Library.Classes.GeneralUtilities;


namespace SqlClientHelperApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        //GetInformation();
        //var (success, item, exception) = SqlLocalInfo();

        //List<float> floats = [1.4f, 2.3f];
        //List<int> ints = [1, 2, 3];
        //Example(ints);
    }

    private static void Example<T>(List<T> list)
    {
        foreach (var item in list)
        {
            if (item is (float or double))
            {
                // do something
            }
            else
            {
                // do nothing
            }
        }
    }

    public void GetInformation()
    {
        var catalog = InitialCatalogFromConnectionString(DataConnections.Instance.MainConnection);
        if (LocalDbDatabaseExists(catalog))
        {
            Dialogs.Information(this, "", $"Catalog {catalog} exist");
        }
        else
        {
            Dialogs.Information(this, "", $"Catalog {catalog} does not exist");

        }
    }

}