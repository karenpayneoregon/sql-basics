namespace DapperWithExcel.Classes;
internal class SqlStatements
{
    public static string GetContactOwners => 
        """
        SELECT C.ContactId,
               C.FullName,
               CD.PhoneNumber
        FROM dbo.Contacts AS C
            INNER JOIN dbo.ContactDevices AS CD
                ON C.ContactId = CD.ContactId
        WHERE (CD.PhoneTypeIdentifier = @PhoneTypeIdentifier)
              AND (C.ContactTypeIdentifier = @ContactTypeIdentifier)
        ORDER BY C.LastName;
        """;
}
