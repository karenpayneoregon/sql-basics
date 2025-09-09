
using System.Diagnostics;
using DateTimeNullDapperApp.Classes;

namespace DateTimeNullDapperApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private async void ReadDataButton_Click(object sender, EventArgs e)
    {
        try
        {
            var results = await DataOperations.ReadDataAsync();
            foreach (var item in results)
            {
                if (item.BirthDate is null)
                {
                    // TODO: Handle null BirthDate
                }
            }
        }
        catch (Exception exception)
        {
            // Log the exception or show a message to the user
        }
    }
}
