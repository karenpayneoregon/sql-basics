#nullable disable

namespace NorthWindSqlLiteApp1.Models
{
    
    /// <summary>
    /// Represents a manager in the NorthWind application, containing information about the manager and their associated workers.
    /// </summary>
    /// <remarks>
    /// This class is used to group employees under a manager, where the manager is represented by the <see cref="Employee"/> property,
    /// and the workers reporting to the manager are represented by the <see cref="Workers"/> property.
    /// </remarks>
    public class Manager
    {
        public Employees Employee { get; set; }
        public List<Employees> Workers { get; set; } = [];
    }
}