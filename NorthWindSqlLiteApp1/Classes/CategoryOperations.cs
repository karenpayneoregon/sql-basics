using Microsoft.EntityFrameworkCore;
using NorthWindSqlLiteApp1.Data;
using NorthWindSqlLiteApp1.Models;
using NorthWindSqlLiteApp1.Models.DTO;

namespace NorthWindSqlLiteApp1.Classes;

/// <summary>
/// Provides operations related to categories, including demonstrations of 
/// implicit and explicit operator conversions between <see cref="Categories"/> 
/// entities and <see cref="CategoryItem"/> DTOs.
/// </summary>
/// <remarks>
/// This class is designed to interact with the database context to retrieve 
/// category data and showcase conversion techniques for transforming entities 
/// into their corresponding data transfer objects (DTOs).
/// </remarks>
/// <seealso cref="Categories"/>
/// <seealso cref="CategoryItem"/>
public class CategoryOperations
{
    /// <summary>
    /// Demonstrates the use of implicit and explicit operators for converting 
    /// <see cref="Categories"/> entities to <see cref="CategoryItem"/> DTOs.
    /// </summary>
    /// <remarks>
    /// This method retrieves a list of categories from the database using Entity Framework 
    /// and showcases two approaches for converting them into <see cref="CategoryItem"/> objects:
    /// implicit conversion and explicit conversion.
    /// </remarks>
    /// <seealso cref="CategoryItem"/>
    /// <seealso cref="Categories"/>
    public static async Task ImplicitExplicitOperatorAsync()
    {
        await using var context = new Context();
        var categories = await context.Categories
            .AsNoTracking()
            .ToListAsync();
        
        List<CategoryItem> implicitList = categories.Select(c => (CategoryItem)c).ToList();
        List<CategoryItem> explicitList = categories.Select<Categories, CategoryItem>(c => c).ToList();
    }
}