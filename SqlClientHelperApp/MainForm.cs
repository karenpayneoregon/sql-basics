
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
        ParseSqlLocalDbInfo();
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
