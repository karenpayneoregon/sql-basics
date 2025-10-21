using System.Data.OleDb;

namespace AccessToExcelApp.Classes;

/// <summary>
/// Provides functionality to map OleDb data types to their corresponding C# types.
/// </summary>
/// <remarks>
/// This class contains a predefined mapping of <see cref="OleDbType"/> values 
/// to their equivalent C# type representations. It is primarily used to convert OleDb type codes 
/// into C# type names for data schema processing.
/// </remarks>
public static class OleDbTypeMapper
{

    private static readonly Dictionary<OleDbType, string> TypeMap = new()
    {
        [OleDbType.BigInt] = "long",
        [OleDbType.Binary] = "byte[]",
        [OleDbType.Boolean] = "bool",
        [OleDbType.BSTR] = "string",
        [OleDbType.Char] = "string",
        [OleDbType.Currency] = "decimal",
        [OleDbType.Date] = "DateTime",
        [OleDbType.DBDate] = "DateTime",
        [OleDbType.DBTime] = "TimeSpan",
        [OleDbType.DBTimeStamp] = "DateTime",
        [OleDbType.Decimal] = "decimal",
        [OleDbType.Double] = "double",
        [OleDbType.Guid] = "Guid",
        [OleDbType.Integer] = "int",
        [OleDbType.LongVarBinary] = "byte[]",
        [OleDbType.LongVarChar] = "string",
        [OleDbType.LongVarWChar] = "string",
        [OleDbType.Numeric] = "decimal",
        [OleDbType.Single] = "float",
        [OleDbType.SmallInt] = "short",
        [OleDbType.TinyInt] = "byte",
        [OleDbType.UnsignedBigInt] = "ulong",
        [OleDbType.UnsignedInt] = "uint",
        [OleDbType.UnsignedSmallInt] = "ushort",
        [OleDbType.UnsignedTinyInt] = "byte",
        [OleDbType.VarBinary] = "byte[]",
        [OleDbType.VarChar] = "string",
        [OleDbType.VarWChar] = "string",
        [OleDbType.WChar] = "string"
    };

    /// <summary>
    /// Maps an <see cref="OleDbType"/> code to its corresponding C# type name.
    /// </summary>
    /// <param name="oleDbTypeCode">
    /// The integer value representing an <see cref="OleDbType"/>.
    /// </param>
    /// <returns>
    /// A string representing the equivalent C# type name. If the provided code does not match
    /// any predefined mapping, the method returns "object".
    /// </returns>
    /// <remarks>
    /// This method is useful for converting OleDb type codes into C# type names, particularly
    /// when working with database schema information.
    /// </remarks>
    public static string ToCSharpType(int oleDbTypeCode)
    {
        var oleDbType = (OleDbType)oleDbTypeCode;
        return TypeMap.GetValueOrDefault(oleDbType, "object");
    }
}
