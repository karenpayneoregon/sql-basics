namespace GlobalConnection.Classes;
public class SqlStatements
{
    public static string CategoryShort() 
        => "SELECT CategoryID,CategoryName FROM dbo.Categories;";

    public static string ContactDemo() =>
        """
        SELECT  ContactId,
                FirstName,
                LastName,
                ContactTypeIdentifier  
        FROM dbo.Contacts 
        WHERE ContactId > @Top
        """;
    public static string CustomersByContactTypeAndCountry()
        => """
                SELECT C.CustomerIdentifier,
                       C.CompanyName,
                       C.ContactId,
                       C.Street,
                       C.City,
                       C.PostalCode,
                       C.CountryIdentifier,
                       C.Phone,
                       C.ContactTypeIdentifier,
                       CT.ContactTitle,
                       Cont.FirstName,
                       Cont.LastName
                FROM dbo.Customers AS C
                    INNER JOIN dbo.Countries AS A
                        ON C.CountryIdentifier = A.CountryIdentifier
                    INNER JOIN dbo.ContactType AS CT
                        ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                    INNER JOIN dbo.Contacts AS Cont
                        ON C.ContactId = Cont.ContactId
                           AND CT.ContactTypeIdentifier = Cont.ContactTypeIdentifier
                WHERE (C.CountryIdentifier = @CountryIdentifier)
                      AND (C.ContactTypeIdentifier = @ContactTypeIdentifier);
                """;

    public static string ProductsCategories() =>
        """
        SELECT 
            ProductID, 
            ProductName, 
            p.CategoryID, 
            QuantityPerUnit,
            CategoryName 
        FROM Products p 
            INNER JOIN Categories c 
                ON p.CategoryID = c.CategoryID
        """;

    public static string ProductsSelect() => """
        SELECT ProductID,
               ProductName,
               SupplierID,
               Products.CategoryID,
               QuantityPerUnit,
               UnitPrice,
               UnitsInStock,
               UnitsOnOrder,
               ReorderLevel,
               Discontinued,
               DiscontinuedDate,
               Categories.CategoryID,
               CategoryName
        FROM dbo.Products
            INNER JOIN dbo.Categories
                ON Products.CategoryID = Categories.CategoryID;
        """;
}
