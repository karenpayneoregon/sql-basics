namespace ProductsCategoriesApp.Classes;
public class SqlStatements
{
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

    public static string CustomerWithContacts1() =>
        """
        SELECT CU.CustomerIdentifier,
               CU.CompanyName,
               CU.ContactId,
               CU.Street,
               CU.City,
               CU.PostalCode,
               CU.CountryIdentifier,
               CU.Phone,
               CU.Fax,
               CU.Region,
               CU.ModifiedDate,
               CU.ContactTypeIdentifier,
               C.ContactId,
               C.FirstName,
               C.LastName,
               C.ContactTypeIdentifier,
               CO.CountryIdentifier ,
               CO.[Name]
        FROM dbo.Customers AS CU
            INNER JOIN dbo.Contacts AS C
                ON CU.ContactId = C.ContactId
            INNER JOIN dbo.Countries AS CO
                ON CU.CountryIdentifier = CO.CountryIdentifier;
    """;

    public static string CustomerWithContacts2() =>
        """
        SELECT CU.CustomerIdentifier,
               CU.CompanyName,
               CU.ContactId,
               CU.Street,
               CU.City,
               CU.PostalCode,
               CU.CountryIdentifier,
               CU.Phone,
               CU.Fax,
               CU.Region,
               CU.ModifiedDate,
               CU.ContactTypeIdentifier,
               C.ContactId,
               C.FirstName,
               C.LastName,
               C.ContactTypeIdentifier,
               CO.CountryIdentifier,
               CO.Name,
               CT.ContactTypeIdentifier,
               CT.ContactTitle
        FROM dbo.Customers AS CU
            INNER JOIN dbo.Contacts AS C
                ON CU.ContactId = C.ContactId
            INNER JOIN dbo.Countries AS CO
                ON CU.CountryIdentifier = CO.CountryIdentifier
            INNER JOIN dbo.ContactType AS CT
                ON CU.ContactTypeIdentifier = CT.ContactTypeIdentifier
                   AND C.ContactTypeIdentifier = CT.ContactTypeIdentifier;
    """;

    public static string Contacts() =>
        """
        SELECT ContactId,
           FirstName,
           LastName,
           Contacts.ContactTypeIdentifier,
           ContactTitle
    FROM dbo.Contacts
        INNER JOIN dbo.ContactType
            ON Contacts.ContactTypeIdentifier = ContactType.ContactTypeIdentifier;
    """;

    public static string ContactsWithDevices() =>
        """
            SELECT      C.ContactId,
                        C.FirstName,
                        C.LastName,
                        C.ContactTypeIdentifier,
                        CT.ContactTitle,
                        CD.id AS DeviceId,
                        CD.ContactId,
                        CD.PhoneTypeIdentifier,
                        CD.PhoneNumber,
                        CT.ContactTypeIdentifier
              FROM      dbo.Contacts AS C
             INNER JOIN dbo.ContactType AS CT
                ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
             INNER JOIN dbo.ContactDevices AS CD
                ON C.ContactId             = CD.ContactId
            WHERE C.ContactId = @ContactId
        """;

    public static string ContactsWithDevicesAndPhoneType() =>
        """
            SELECT C.ContactId,
                   C.FirstName,
                   C.LastName,
                   C.ContactTypeIdentifier,
                   CT.ContactTitle,
                   CD.id AS DeviceId,
                   CD.ContactId,
                   CD.PhoneTypeIdentifier,
                   CD.PhoneNumber,
                   CT.ContactTypeIdentifier,
                   PT.PhoneTypeIdenitfier AS PhoneTypeIdentifier,
                   PT.PhoneTypeDescription
            FROM dbo.Contacts AS C
                INNER JOIN dbo.ContactType AS CT
                    ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                INNER JOIN dbo.ContactDevices AS CD
                    ON C.ContactId = CD.ContactId
                INNER JOIN dbo.PhoneType AS PT
                    ON CD.PhoneTypeIdentifier = PT.PhoneTypeIdenitfier
            WHERE PT.PhoneTypeIdenitfier = @PhoneTypeIdenitfier AND CD.Id IS NOT NULL;
    """;

    public static string ContactsWithDevicesAndPhoneType1() =>
        """
                SELECT C.ContactId,
                       C.FirstName,
                       C.LastName,
                       C.ContactTypeIdentifier,
                       CT.ContactTitle,
                       CD.id AS DeviceId,
                       CD.ContactId,
                       CD.PhoneTypeIdentifier,
                       CD.PhoneNumber,
                       CT.ContactTypeIdentifier,
                       PT.PhoneTypeIdenitfier AS PhoneTypeIdentifier,
                       PT.PhoneTypeDescription
                FROM dbo.Contacts AS C
                    INNER JOIN dbo.ContactType AS CT
                        ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                    INNER JOIN dbo.ContactDevices AS CD
                        ON C.ContactId = CD.ContactId
                    INNER JOIN dbo.PhoneType AS PT
                        ON CD.PhoneTypeIdentifier = PT.PhoneTypeIdenitfier
                WHERE PT.PhoneTypeIdenitfier = @PhoneTypeIdenitfier;
        """;
}
