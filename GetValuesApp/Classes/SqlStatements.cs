namespace GetValuesApp.Classes;
internal class SqlStatements
{
    public static string ReadCustomers() 
        => """
         SELECT C.CustomerIdentifier,
                C.CompanyName,
                C.ContactId,
                C.ContactTypeIdentifier,
                CT.ContactTitle,
                CO.FullName
         FROM dbo.Customers AS C
             INNER JOIN dbo.Contacts AS CO
                 ON C.ContactId = CO.ContactId
             INNER JOIN dbo.ContactType AS CT
                 ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                    AND CO.ContactTypeIdentifier = CT.ContactTypeIdentifier;
         """;
    public static string ReadCustomersOwners()
        => """
           SELECT C.CustomerIdentifier,
                  C.CompanyName,
                  C.ContactId,
                  C.ContactTypeIdentifier,
                  CT.ContactTitle,
                  CO.FullName
           FROM dbo.Customers AS C
               INNER JOIN dbo.Contacts AS CO
                   ON C.ContactId = CO.ContactId
               INNER JOIN dbo.ContactType AS CT
                   ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
                      AND CO.ContactTypeIdentifier = CT.ContactTypeIdentifier
           WHERE (C.ContactTypeIdentifier = @ContactTypeIdentifier)
           ORDER BY C.CompanyName;
           """;
}
