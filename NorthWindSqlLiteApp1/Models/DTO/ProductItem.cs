using NorthWindSqlLiteApp1.Models;

namespace NorthWindSqlLiteApp1.Models.DTO;

#pragma warning disable CS8618

public class ProductItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public Categories Categories { get; set; }
    // An init enforces immutability, so that once the object is initialized, it can't be changed.
    public string CategoryName { get; init; }

    public override string ToString() => ProductName;

    /// <summary>
    /// Converts an instance of <see cref="Products"/> to an instance of <see cref="ProductItem"/>.
    /// </summary>
    /// <param name="product">The <see cref="Products"/> instance to convert.</param>
    /// <returns>A new instance of <see cref="ProductItem"/> initialized with the values from the specified <see cref="Products"/> instance.</returns>
    public static implicit operator ProductItem(Products product)
    {
        return new ProductItem
        {
            ProductId = product.ProductID,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock,
            Categories = product.Category,
            CategoryName = product.Category?.CategoryName ?? "N/A"
        };
    }
}


