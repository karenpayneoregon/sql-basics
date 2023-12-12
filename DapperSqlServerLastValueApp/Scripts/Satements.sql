--- 
/****** 
    Sample for LAST_VALUE
    https://learn.microsoft.com/en-us/sql/t-sql/functions/last-value-transact-sql?view=sql-server-ver16
    For NorthWind2023 or NorthWind2024
******/



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



