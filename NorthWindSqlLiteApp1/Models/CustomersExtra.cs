namespace NorthWindSqlLiteApp1.Models;
public partial class Customers
{
    public override string ToString() => $"{CustomerIdentifier,-14}{CompanyName,-40}{ModifiedDate:D}";


}
