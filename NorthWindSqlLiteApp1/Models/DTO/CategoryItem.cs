#nullable disable
namespace NorthWindSqlLiteApp1.Models.DTO;
/// <summary>
/// Represents a data transfer object (DTO) for a category item.
/// </summary>
/// <remarks>
/// This class provides a simplified representation of a category, 
/// including its identifier and name, and supports implicit conversion 
/// from the <see cref="Categories"/> entity.
/// </remarks>
public class CategoryItem
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    /// <summary>
    /// Defines an implicit conversion from a <see cref="Categories"/> entity to a <see cref="CategoryItem"/> DTO.
    /// </summary>
    /// <param name="category">The <see cref="Categories"/> entity to be converted.</param>
    /// <returns>A new instance of <see cref="CategoryItem"/> populated with data from the specified <see cref="Categories"/> entity.</returns>
    /// <remarks>
    /// This conversion simplifies the transformation of a <see cref="Categories"/> entity into a lightweight 
    /// data transfer object (<see cref="CategoryItem"/>) for use in scenarios where only basic category information 
    /// is required.
    /// </remarks>
    public static implicit operator CategoryItem(Categories category)
    {
        return new CategoryItem
        {
            CategoryId = category.CategoryID,
            CategoryName = category.CategoryName
        };
    }
}

