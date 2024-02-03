namespace DapperNorthWind2024.Models;
public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public override string ToString() => CategoryName;

}