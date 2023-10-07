namespace RowFilterApp.Classes;
internal class SqlStatements
{
    public static string CompanyNameContains =>
        """
               SELECT c.CustomerIdentifier,
               c.CompanyName,
               c.ContactId,
               ct.FirstName,
               ct.LastName,
               c.ContactTypeIdentifier
        FROM dbo.Customers AS c
            INNER JOIN dbo.Contacts AS ct
                ON c.ContactId = ct.ContactId 
        """;
}
