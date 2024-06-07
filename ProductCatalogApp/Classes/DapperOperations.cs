using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Bogus;
using Dapper;
using ProductCatalogApp.Models;
using static System.DateTime;

namespace ProductCatalogApp.Classes;
internal class DapperOperations
{
    /// <summary>
    /// Add new product to the database using Bogus
    /// </summary>
    public static void AddNewProduct()
    {
        static bool Decide() => Now.Second.IsEven();

        using SqlConnection cn = new(ConnectionString());

        string[] tags = ["promo", "Special"];

        if (Decide())
        {
            tags = ["new"];
        }

        var product = FakeProduct();

        cn.Execute("""
                   INSERT INTO Product (Name, Color, Price, Quantity, Size, Data, Tags) 
                   VALUES (@Name, @Color, @Price, @Quantity, @Size, @Data, @Tags)
                   """, 
            new
            {
                product.Name,
                product.Color,
                product.Price,
                product.Quantity,
                product.Size,
                product.Data,
                Tags = JsonSerializer.Serialize(tags)
            });
    }

    /// <summary>
    /// Create a random product using Bogus NuGet package
    /// </summary>
    public static Product FakeProduct()
    {
        var manufacturing = new Faker<Manufacturing>()
            .RuleFor(m => m.ManufacturingCost, f => f.Random.Float(1, 100))
            .RuleFor(m => m.Type, f => f.Commerce.ProductAdjective())
            .RuleFor(m => m.MadeIn, f => f.Address.Country());

        var product = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Color, f => f.Commerce.Color())
            .RuleFor(p => p.Price, f => f.Random.Double(1, 100))
            .RuleFor(p => p.Quantity, f => f.Random.Number(1, 100))
            .RuleFor(p => p.Size, f => "LG");

        var singleProduct =  product.Generate();
        singleProduct.Data = JsonSerializer.Serialize(manufacturing.Generate());
        
        return singleProduct;

    }

    /// <summary>
    /// Get product by primary key
    /// </summary>
    /// <param name="id">Primary key</param>
    /// <remarks>
    /// Also consider using a stored procedure
    /// </remarks>
    public static Product GetProduct(int id)
    {
        using SqlConnection cn = new(ConnectionString());
        return cn.QueryFirstOrDefault<Product>("SELECT * FROM Product WHERE ProductID = @id", new { id });
    }
    /// <summary>
    /// Delete product by primary key
    /// </summary>
    /// <param name="id">Primary key</param>
    /// <remarks>
    /// Also consider using a stored procedure
    /// </remarks>
    public static void DeleteProduct(int id) 
    {
        using SqlConnection cn = new(ConnectionString());
        cn.Execute("DELETE FROM Product WHERE ProductID = @id", new { id });
    }
    /// <summary>
    /// Update product using stored procedure
    /// </summary>
    /// <param name="product">Valid product</param>
    /// <remarks>
    /// Also consider using a stored procedure
    /// </remarks>
    public static void UpdateProduct(Product product)
    {
        using SqlConnection cn = new(ConnectionString());
        cn.Execute("UpdateProductFromJson", product, commandType: CommandType.StoredProcedure);
    }
}
