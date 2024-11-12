USE [NorthWind2024]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetExtendedPropertiesForUserStoredProcedures] AS
BEGIN
SELECT      S.name AS [Schema],
            O.name AS [Name],
            EP.name [Information],
            EP.value AS [PropertyValue]
  FROM      sys.extended_properties EP
  LEFT JOIN sys.all_objects O
    ON EP.major_id = O.object_id
  LEFT JOIN sys.schemas S
    ON O.schema_id = S.schema_id
  LEFT JOIN sys.columns AS c
    ON EP.major_id = c.object_id
   AND EP.minor_id = c.column_id
 WHERE      O.name LIKE 'usp%';
END

GO