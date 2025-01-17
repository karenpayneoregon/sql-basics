using ProductsCategoriesApp1.Classes.Configuration;
using SqlServer.Library.Classes;
using DataOperations = ProductsCategoriesApp1.Classes.DataOperations;

namespace ProductsCategoriesApp1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();

        var customers = (await DataOperations.GetCustomerDetails()).ToList();
        var customer = await DataOperations.GetCustomer(1);


        var singleContact = await DataOperations.GetContactByIdAsync(1);
        var all = GeneralUtilities.TablesArePopulated1(
            DataConnections.Instance.MainConnection);

        var details = GeneralUtilities.TablesCount(
            DataConnections.Instance.MainConnection);

        ExitPrompt();
    }
}