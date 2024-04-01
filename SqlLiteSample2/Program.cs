using SqlLiteSample2.Classes;

namespace SqlLiteSample2;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        
        await EntityOperations.ReadAllCustomers();
        await EntityOperations.AddNewContact();
        await EntityOperations.AddNewContactBad();
        await EntityOperations.ReadAllCategories();
        await EntityOperations.ReadAllEmployeesUpDateOne();
        await EntityOperations.RemoveOneOrder();
        await EntityOperations.GetTableNamesInDatabase();
        await EntityOperations.UpdateCustomerCity();

        SpectreConsoleHelpers.ExitPrompt();
    }


}