using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalConnection.Models;

public class Product
{
    public int Id => ProductID;
    public int ProductID { get; set; }

    public string ProductName { get; set; }

    // Maps to the foreign key column
    public int CategoryID { get; set; }

    public string QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }

    public DateTime? DiscontinuedDate { get; set; }

    // The navigation property
    public Category Category { get; set; }
    public override string ToString() => ProductName;

}

