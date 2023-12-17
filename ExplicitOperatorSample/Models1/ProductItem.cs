namespace ExplicitOperatorSample.Models1;

#pragma warning disable CS8618

public class ProductItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public short UnitsInStock { get; set; }

    public override string ToString() => ProductName;

    public static explicit operator ProductItem(Product item)
    {
        return new ProductItem
        {
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            UnitPrice = item.UnitPrice,
            UnitsInStock = item.UnitsInStock,
        };
    }
}


