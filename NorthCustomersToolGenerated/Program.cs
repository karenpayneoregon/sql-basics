using SqlServer.Library.Classes;
using static NorthCustomersToolGenerated.Classes.Configuration.DataConnections;
using DataOperations = NorthCustomersToolGenerated.Classes.DataOperations;

namespace NorthCustomersToolGenerated;
internal partial class Program
{

    static async Task Main(string[] args)
    {
        await Setup();

        var customers = (await DataOperations.GetCustomerDetails()).ToList();
        var customer = await DataOperations.GetCustomer(1);
        
        var singleContact = await DataOperations.GetContactByIdAsync(1);

        var all = GeneralUtilities.TablesArePopulated1(Instance.MainConnection);

        var details = GeneralUtilities.TablesCount(Instance.MainConnection);

        ExitPrompt();
    }
}