
using DapperSimpleEfCoreApp.Properties;

namespace DapperSimpleEfCoreApp.Classes;


public sealed class CoreBindingNavigator : BindingNavigator
{
    /// <summary>
    /// Used for removing current item
    /// </summary>
    public ToolStripButton RemoveButton { get; set; } = new();

    public CoreBindingNavigator()
    {
        AddStandardItems();

        AddNewItem!.Visible = false;
        DeleteItem!.Visible = false;
        RemoveButton.Image = Resources.Delete1;
        Items.Add(RemoveButton);
    }
}