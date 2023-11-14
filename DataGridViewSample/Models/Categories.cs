namespace DataGridViewSample.Models;
public class Categories : IEquatable<Categories>
{
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public bool Equals(Categories other) => CategoryId == other?.CategoryId;
}
