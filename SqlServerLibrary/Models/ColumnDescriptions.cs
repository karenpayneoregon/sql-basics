
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SqlServerLibrary.Models;

public class ColumnDescriptions
{
    /// <summary>
    /// Name of column
    /// </summary>
    /// <returns></returns>
    public string Name { get; set; }
    /// <summary>
    /// Ordinal position of column
    /// </summary>
    /// <returns></returns>
    public int Ordinal { get; set; }
    /// <summary>
    /// Description of column
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// May be NULL
    /// </remarks>
    public string Description { get; set; }

}