namespace DapperSqlServerLastValueApp.Classes;
internal class SqlStatements
{
    public static string SelectStatement => 
        """
        SELECT s.CompanyName,
               p.ProductName,
               p.UnitPrice,
               LAST_VALUE(p.UnitPrice) OVER (PARTITION BY p.SupplierID
                                             ORDER BY p.UnitPrice
                                             ROWS BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING
                                            ) AS LastValue
        FROM dbo.Products p
            INNER JOIN dbo.Suppliers s
                ON p.SupplierID = s.SupplierID ORDER BY s.CompanyName;
        """;
}
