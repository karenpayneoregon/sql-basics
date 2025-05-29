namespace DapperMultipleInsertsApp.Models;

/// <summary>
/// Represents a category entity with properties for its identifier, name, and description.
/// </summary>
public class Categories
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
}