
#nullable disable
namespace NorthWindSqlLiteApp1.Models.Interceptor;
/// <summary>
/// Represents a model used to compare the original and new values of an entity,
/// along with its associated state during change tracking.
/// </summary>
/// <remarks>
/// This class is primarily used to capture and store the differences between
/// the original and updated values of an entity, as well as its state
/// (e.g., Added, Deleted, Modified) during operations such as auditing or logging.
/// </remarks>
public class CompareModel
{
    public object OriginalValue { get; set; }

    public object NewValue { get; set; }
    public string EntityState { get; set; }
}
