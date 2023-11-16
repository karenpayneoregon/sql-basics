#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace ConsoleApp1.Models;

#pragma warning disable CS8603
public class ProductItem
{
    public int Id { get; set; }
    public int ColorId { get; set; }
    public string Item { get; set; }

}