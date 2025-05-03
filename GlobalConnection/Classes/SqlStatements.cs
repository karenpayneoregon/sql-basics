namespace GlobalConnection.Classes;
public class SqlStatements
{
    /// <summary>
    /// Generates a SQL query to retrieve category IDs and names from the "Categories" table.
    /// </summary>
    /// <returns>A SQL query string to select "CategoryID" and "CategoryName" columns from the "dbo.Categories" table.</returns>
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
        
    /// <summary>
    /// Generates a SQL query to retrieve customer details filtered by contact type and country.
    /// </summary>
    /// <remarks>
    /// The query joins the "Customers", "Countries", "ContactType", and "Contacts" tables to fetch
    /// customer information, including identifiers, company name, address, contact details, and contact type.
    /// </remarks>
    /// <returns>
    /// A SQL query string that selects customer details where the country identifier and contact type identifier
    /// match the specified parameters.
    /// </returns>
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
        SELECT p.ProductID,
               p.ProductName,
               p.CategoryID,
        	   c.CategoryID,
               p.QuantityPerUnit,
               c.CategoryName
        FROM dbo.Products p
            INNER JOIN dbo.Categories c
                ON p.CategoryID = c.CategoryID;
        """;

    public static string ProductsSelect() => """
        SELECT P.ProductID,
               P.ProductName,
               P.SupplierID,
               P.CategoryID,
               P.QuantityPerUnit,
               P.UnitPrice,
               P.UnitsInStock,
               P.UnitsOnOrder,
               P.ReorderLevel,
               P.Discontinued,
               P.DiscontinuedDate,
               C.CategoryName
        FROM dbo.Products AS P
            INNER JOIN dbo.Categories AS C
                ON P.CategoryID = C.CategoryID;
        """;
}
