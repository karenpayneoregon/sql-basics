using Microsoft.EntityFrameworkCore;

namespace NorthWindSqlLiteApp1.Classes.Extensions;
public static class EntityExtensions
{
    /// <summary>
    /// Retrieves a list of CLR types representing the entities defined in the model of the specified <see cref="DbContext"/>.
    /// </summary>
    /// <param name="context">
    /// The <see cref="DbContext"/> instance from which to retrieve the entity types.
    /// </param>
    /// <returns>
    /// A list of <see cref="Type"/> objects representing the CLR types of the entities in the model.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="context"/> is <c>null</c>.
    /// </exception>
    public static List<Type> GetModelTypes(this DbContext context)
    {
        return context == null
            ? throw new ArgumentNullException(nameof(context))
            : context
                .Model
                .GetEntityTypes()
                .Select(et => et.ClrType)
                .Where(t => t is not null)
                .Where(t =>
                    // Exclude Dictionary<TKey, TValue> (shows up as Dictionary`2)
                    !(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>)) &&
                    // Exclude anything implementing IDictionary<TKey, TValue>
                    !t.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
                .Distinct()
                .ToList();
    }
}


