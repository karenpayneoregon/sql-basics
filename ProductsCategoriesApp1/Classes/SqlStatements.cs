namespace ProductsCategoriesApp1.Classes;
internal class SqlStatements
{
    public static string CustomerWithContacts() =>
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

}
