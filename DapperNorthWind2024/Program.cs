using DapperNorthWind2024.Classes;
using DapperNorthWind2024.Models;

namespace DapperNorthWind2024;

internal partial class Program
{

    static void Main(string[] args)
    {

        Operations operations = new();

        List<Category> categories = operations.Categories();
        Category category = Random.Shared.GetItems( categories.ToArray(), 1)[0];
        Console.WriteLine(category.CategoryName);
        List<Product> products = operations.GetAllProductsByCategory(category.CategoryId);

        var test = operations.GetAllProductsBySupplier();
        Console.ReadLine();
    }

}