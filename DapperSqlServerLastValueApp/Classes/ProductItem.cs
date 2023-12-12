namespace DapperSqlServerLastValueApp.Classes;
public class ProductItem
{
    public string CompanyName { get; set; }
    public string ProductName { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? LastValue { get; set; }
    public override string ToString() => $"{CompanyName}, {ProductName}";

}
