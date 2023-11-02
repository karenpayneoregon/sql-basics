using Microsoft.Data.SqlClient;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using Dapper;
using GlobalConnection.Models;

namespace GlobalConnection.Classes;
public class Operations
{
    /// <summary>
    /// Retrieves a dictionary of category IDs and names from the database.
    /// </summary>
    /// <returns>A dictionary with category IDs as keys and category names as values.</returns>
    public static async Task<Dictionary<int, string>> Categories()
    {
        await using SqlConnection cn = new(ConnectionString());
        Dictionary<int, string> dict = cn.Query(SqlStatements.CategoryShort()).ToDictionary(
            row => (int)row.CategoryID,
            row => (string)row.CategoryName);

        return dict;
    }

    public static async Task<List<Category>> CategoriesList()
    {
        await using SqlConnection cn = new(ConnectionString());
        var result = await cn.QueryAsync<Category>(SqlStatements.CategoryShort());
        return result.ToList();
    }



    public static async Task<List<Product>> ProductsWithCategory()
    {
        await using SqlConnection cn = new(ConnectionString());

        IEnumerable<Product> products = await cn.QueryAsync<Product, Category, Product>(
            SqlStatements.ProductsCategories(), (product, category) => {
                product.Category = category;
                return product;
            },
            splitOn: "CategoryId");

        return products.ToList();

    }
}
