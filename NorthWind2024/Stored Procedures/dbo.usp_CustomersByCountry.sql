SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[usp_CustomersByCountry] (@CountryIdentifier AS INT) AS
BEGIN
SELECT      cust.CustomerIdentifier,
            cust.CompanyName,
            cust.ContactId,
            c.FullName,
            cust.CountryIdentifier,
            cust.Phone,
            cust.ContactTypeIdentifier,
            co.Name
  FROM      dbo.Customers AS cust
 INNER JOIN dbo.Countries AS co
    ON cust.CountryIdentifier = co.CountryIdentifier
 INNER JOIN dbo.Contacts AS c
    ON cust.ContactId  = c.ContactId
 WHERE      (cust.CountryIdentifier = @CountryIdentifier);
END

GO
