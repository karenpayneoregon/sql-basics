namespace ProductsCategoriesApp1.Models;

/// <summary>
/// Represents a country entity with a unique identifier and a name.
/// </summary>
/// <remarks>
/// This class is used to model country data, which can be associated with other entities 
/// such as <see cref="Customer"/>.
/// </remarks>
public class Country
{
    public int CountryIdentifier { get; set; }
    public string Name { get; set; }
}