using NorthWindSqlLiteApp1.Classes.Core;

namespace NorthWindSqlLiteApp1.Models.Sorting;
/// <summary>
/// Represents the properties of a customer that can be used for sorting operations.
/// </summary>
/// <remarks>
/// This enumeration is primarily used in conjunction with sorting methods, such as 
/// <see cref="OrderingHelpers.OrderByEnum"/> to dynamically determine the property to sort by.
/// </remarks>
/// <summary>
/// Sort by the customer's first name.
/// </summary>
/// <summary>
/// Sort by the customer's last name.
/// </summary>
/// <summary>
/// Sort by the name of the customer's country.
/// </summary>
/// <summary>
/// Sort by the customer's title.
/// </summary>
public enum PropertyName
{
    /// <summary>
    /// Contact first name
    /// </summary>
    FirstName,
    /// <summary>
    /// Contact last name
    /// </summary>
    LastName,
    /// <summary>
    /// Customer country name
    /// </summary>
    CountryName,
    /// <summary>
    /// Contact title
    /// </summary>
    Title
}