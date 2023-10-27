
namespace InsertNewRecordApp.Classes;


public sealed class CoreBindingNavigator : BindingNavigator
{
    public CoreBindingNavigator()
    {
        AddStandardItems();
        AddNewItem.Enabled = false;
        DeleteItem.Enabled = false;
    }
}