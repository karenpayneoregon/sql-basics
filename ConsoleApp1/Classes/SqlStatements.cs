namespace ConsoleApp1.Classes;
internal class SqlStatements
{
    /// <summary>
    /// Get all records from Person table
    /// </summary>
    public static string ReadPeople =>
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM dbo.Person;
        """;
    public static string GetPersonById =>
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM dbo.Person
        WHERE Id = @Id
        """;

    public static string OrdersPagination() =>
        """
        SELECT o.OrderID,
               o.CustomerIdentifier,
               o.EmployeeID,
               o.OrderDate,
               o.ShippedDate,
               o.DeliveredDate,
               o.ShipVia,
               o.Freight, 
        	   s.CompanyName AS Shipper,
        	   e.FirstName + ' ' + e.LastName AS FullName
        FROM dbo.Orders AS o
        INNER JOIN dbo.Shippers AS s
                ON o.ShipVia = s.ShipperID
            INNER JOIN dbo.Employees AS e
                ON o.EmployeeID = e.EmployeeID
        ORDER BY o.OrderID 
        OFFSET @OffSet ROWS FETCH NEXT @PageSize ROW ONLY;
        """;
}
