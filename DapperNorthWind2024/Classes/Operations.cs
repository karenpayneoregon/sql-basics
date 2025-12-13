using System.Data;
using Dapper;
using DapperNorthWind2024.Models;
using Microsoft.Data.SqlClient;

namespace DapperNorthWind2024.Classes;

public class Operations
{
    private IDbConnection _db = new SqlConnection(ConnectionString());

    public int CategoryCount()
    {
        return _db.QuerySingle<int>("SELECT COUNT(CategoryID) FROM dbo.Categories");
    }

    public List<Category> CategoryList()
    {
        var statement =
            """
            SELECT CategoryID
                ,CategoryName
                ,[Description]
            FROM dbo.Categories
            """;

        return _db.Query<Category>(statement).AsList();
    }

    private List<Countries> CountriesList()
    {
        List<Countries> countries = _db.Query<Countries>(
                """
                SELECT [CountryIdentifier],[Name]
                FROM [dbo].[Countries];
                """)
            .AsList();
        return countries;
    }

public async Task<List<ProcedureProperty>> ProcedureProperties()
{

    var name = "usp_GetExtendedPropertiesForUserStoredProcedures";
    return  (await _db.QueryAsync<ProcedureProperty>(name, commandType: CommandType.StoredProcedure)).AsList();
}

    public List<Product> GetAllProductsByCategory(int categoryId)
    {
        var categories = CategoryList();
        var countries = CountriesList();

        string statement = 
            """
             SELECT p.ProductID,
                    p.ProductName,
                    p.[CategoryID],
                    c.[CategoryID],
                    c.CategoryName,
                    c.Description,
                    s.[SupplierID],
                    s.CompanyName,
                    s.City,
                    s.CountryIdentifier
             FROM dbo.Products p
                 LEFT JOIN dbo.Categories c ON p.[CategoryID] = c.[CategoryID]
                 LEFT JOIN dbo.Suppliers s ON p.SupplierID = s.SupplierID 
             WHERE p.[CategoryId] = @CategoryId 
             ORDER BY p.[ProductName]
             """;

        var result = _db.Query<Product, Category, Supplier, Product>(statement, param: new { CategoryId = categoryId },
            map: (product, category, supplier) =>
            {

                supplier.Country = countries.FirstOrDefault(x => x.CountryIdentifier == supplier.CountryIdentifier)?.Name;
                product.Category = categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                product.CategoryId = categoryId;
                product.Supplier = supplier;

                return product;
            },
            splitOn: nameof(Product.CategoryId)).AsList();

        return result;
    }

    public List<Product> GetAllProductsBySupplier(int supplierId = 2)
    {
        var categories = CategoryList();
        var countries = CountriesList();

        const string statement =
            """
            SELECT dbo.Products.*,
                   dbo.Suppliers.*
            FROM dbo.Products 
            JOIN dbo.Suppliers ON Products.SupplierID = Suppliers.SupplierID AND Suppliers.SupplierID = @supplierId;
            """;

        IEnumerable<Product> products = _db.Query<Product, Supplier, Product>(statement, (product, supplier) => 
            {
                    supplier.Country = countries.FirstOrDefault(x => x.CountryIdentifier == supplier.SupplierId)!.Name;
                    Category localCategory = categories.FirstOrDefault(x => x.CategoryId == product.CategoryId);
                    product.Supplier = supplier;
                    product.Category = localCategory;

                    return product;
            },
                new { supplierId },
                splitOn: nameof(Supplier.SupplierId));

        return products.AsList();
    }
    
}

