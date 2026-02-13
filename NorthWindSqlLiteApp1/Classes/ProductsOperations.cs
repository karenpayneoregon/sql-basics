using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Models;
using NorthWindSqlLiteApp1.Models.DTO;

namespace NorthWindSqlLiteApp1.Classes;
/// <summary>
/// Provides operations related to products, including demonstrations of 
/// implicit and explicit operator conversions between <see cref="Products"/> 
/// and <see cref="ProductItem"/>.
/// </summary>
/// <remarks>
/// This class contains methods that interact with the database context to retrieve 
/// product data and convert it into corresponding DTOs. It showcases the usage of 
/// implicit and explicit operators for transforming entities into their DTO representations.
/// </remarks>
/// <seealso cref="Products"/>
/// <seealso cref="ProductItem"/>
internal class ProductsOperations
{
    /// <summary>
    /// Demonstrates the usage of implicit operator conversions between <see cref="Products"/> 
    /// and <see cref="ProductItem"/>.
    /// </summary>
    /// <remarks>
    /// This method retrieves a list of products from the database context and converts them 
    /// into a list of <see cref="ProductItem"/> objects using two different approaches:
    /// <list type="bullet">
    /// <item>Implicit operator conversion.</item>
    /// <item>Explicit LINQ projection.</item>
    /// </list>
    /// </remarks>
    /// <seealso cref="ProductItem"/>
    /// <seealso cref="Products"/>
    public static async Task ImplicitOperator()
    {
        await using var context = new Context();
        var products = await context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();
        
        List<ProductItem> implicitList = products.Select(p => (ProductItem)p).ToList();
        List<ProductItem> explicitList = products.Select<Products, ProductItem>(p => p).ToList();
    }
}

