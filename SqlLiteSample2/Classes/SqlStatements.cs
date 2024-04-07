namespace SqlLiteSample2.Classes;
internal class SqlStatements
{
    public static string CustomerJoined() =>
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
            FROM Customers AS CU
                INNER JOIN Contacts AS C
                    ON CU.ContactId = C.ContactId
                INNER JOIN Countries AS CO
                    ON CU.CountryIdentifier = CO.CountryIdentifier;
        """;

    public static string CustomerJoined1() =>
        """
        SELECT     CU.CompanyName,
                   CU.Street,
                   CU.City,
                   CU.PostalCode,
                   CU.CountryIdentifier,
                   CU.Phone,
                   CU.Fax,
                   CU.Region,
                   CU.ModifiedDate,
                   CU.ContactTypeIdentifier,
                   C.ContactId ,
                   C.FirstName,
                   C.LastName,
                   C.ContactTypeIdentifier AS Expr2,
                   CO.CountryIdentifier AS Expr3,
                   CO.Name,
                   CT.ContactTitle
         FROM      Customers AS CU
        INNER JOIN Contacts AS C
           ON CU.ContactId         = C.ContactId
        INNER JOIN Countries AS CO
           ON CU.CountryIdentifier     = CO.CountryIdentifier
        INNER JOIN dbo.ContactType AS CT
           ON CU.ContactTypeIdentifier = CT.ContactTypeIdentifier
          AND C.ContactTypeIdentifier  = CT.ContactTypeIdentifier;
        """;
}
