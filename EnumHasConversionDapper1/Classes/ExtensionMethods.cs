using EnumHasConversionDapper1.Models;
using Microsoft.Data.SqlClient;

namespace EnumHasConversionDapper1.Classes;

public static class ExtensionMethods
{
    /// <summary>
    /// Get ordinal value for a <see cref="Enum"/>
    /// </summary>
    /// <param name="sender">Enum member</param>
    /// <returns>Ordinal value for member</returns>
    public static int IntValue(this Enum sender) 
        => Convert.ToInt32(sender);

    public static void ToWineType(this SqlDataReader reader, int index)
    {
        var value = (WineType)reader.GetInt32(index);
    }
}