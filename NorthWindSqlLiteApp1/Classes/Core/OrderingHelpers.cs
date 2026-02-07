using NorthWindSqlLiteApp1.Models.Sorting;
using System.Linq.Expressions;
using NorthWindSqlLiteApp1.Models;

namespace NorthWindSqlLiteApp1.Classes.Core;
public static class OrderingHelpers
{
    /// <summary>
    /// Orders the specified <see cref="IQueryable{T}"/> of <see cref="Customers"/> based on the provided property and direction.
    /// </summary>
    /// <param name="query">The queryable collection of <see cref="Customers"/> to be ordered.</param>
    /// <param name="key">
    /// The property of <see cref="Customers"/> to order by. 
    /// Must be one of the values defined in <see cref="PropertyName"/>.
    /// </param>
    /// <param name="direction">
    /// The direction of the ordering. Defaults to <see cref="Direction.Ascending"/> if not specified.
    /// </param>
    /// <returns>
    /// A new <see cref="IQueryable{T}"/> of <see cref="Customers"/> ordered by the specified property and direction.
    /// </returns>
    /// <remarks>
    /// This method uses expressions to dynamically determine the property to sort by based on the provided <paramref name="key"/>.
    /// </remarks>
    /// <example>
    /// The following example demonstrates how to use this method to sort customers by their contact title in descending order:
    /// <code>
    /// var sortedCustomers = context.Customers
    ///     .OrderByEnum(PropertyName.Title, Direction.Descending)
    ///     .ToList();
    /// </code>
    /// </example>
    public static IQueryable<Customers> OrderByEnum(this IQueryable<Customers> query, PropertyName key, Direction
        direction = Direction.Ascending)
    {
        Expression<Func<Customers, object>> exp = key switch
        {
            PropertyName.LastName => customer => customer.Contact.LastName,
            PropertyName.FirstName => customer => customer.Contact.FirstName,
            PropertyName.CountryName => customer => customer.CountryIdentifierNavigation.Name,
            PropertyName.Title => customer => customer. ContactTypeIdentifierNavigation.ContactTitle,
            _ => customer => customer.CompanyName
        };

        return direction == Direction.Ascending ? query.OrderBy(exp) : query.OrderByDescending(exp);

    }
}
