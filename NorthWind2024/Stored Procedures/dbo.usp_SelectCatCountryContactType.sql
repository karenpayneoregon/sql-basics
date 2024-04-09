SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[usp_SelectCatCountryContactType]
AS
BEGIN
    SELECT CategoryID,
           CategoryName
      FROM dbo.Categories;
    SELECT ContactTypeIdentifier,
           ContactTitle
      FROM dbo.ContactType;
    SELECT CountryIdentifier,
           [Name]
      FROM dbo.Countries;
END;

GO
