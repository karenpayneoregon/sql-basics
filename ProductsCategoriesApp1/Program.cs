
using ProductsCategoriesApp1.Classes;

namespace ProductsCategoriesApp1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();

        var customers = (await DataOperations.GetCustomerDetails()).ToList();
        var singleContact = await DataOperations.GetContactByIdAsync(1);
        Console.WriteLine(customers.Count);
        ExitPrompt();
    }
}