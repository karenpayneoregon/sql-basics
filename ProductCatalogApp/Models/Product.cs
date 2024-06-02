namespace ProductCatalogApp.Models;

public class Product
{
    public int ProductID;
    public string Name;
    public string Color;
    public string Size;
    public double Price;
    public int Quantity;
    public string Data;
    public string[] Tags;
    public override string ToString() => Name;

}

