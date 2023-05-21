using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalConnection.Classes;
public class SqlStatements
{
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
}
