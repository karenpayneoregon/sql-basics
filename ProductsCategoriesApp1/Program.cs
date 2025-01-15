
using ProductsCategoriesApp1.Classes;

namespace ProductsCategoriesApp1;
internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Setup();

        var result = DataOperations.GetCustomerDetails().ToList();
        var contact = await DataOperations.GetContactByIdAsync(1);
        Console.WriteLine(result.Count);
        ExitPrompt();
    }
}