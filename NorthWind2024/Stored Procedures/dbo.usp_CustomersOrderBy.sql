SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[usp_CustomersOrderBy] (@orderby AS VARCHAR(255))
AS
BEGIN
    SELECT CustomerIdentifier,
           CompanyName
      FROM NorthWind2022.dbo.Customers
     ORDER BY CASE
                   WHEN @orderby = 'CompanyName ASC' THEN CompanyName
                   ELSE NULL END ASC,
              CASE
                   WHEN @orderby = 'CompanyName DESC' THEN CompanyName
                   ELSE NULL END DESC;
END;

GO
