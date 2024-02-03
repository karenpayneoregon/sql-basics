namespace DapperNorthWind2024.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public Supplier Supplier { get; set; }
    public override string ToString() => ProductName;

}