/****** 
For testing prior to inserting into SqlStatements class
******/

--- Example that inserts a record and returns the new records primary key
USE InsertExamples;
GO

DECLARE @FirstName AS NVARCHAR(20);
DECLARE @LastName AS NVARCHAR(20);
DECLARE @BirthDate AS DATE;

INSERT INTO dbo.Person
(
    FirstName,
    LastName,
    BirthDate
)
VALUES
(@FirstName, @LastName, @BirthDate);
SELECT CAST(SCOPE_IDENTITY() AS INT);

--- Use with care
DELETE FROM dbo.Person
DBCC CHECKIDENT (Person, RESEED, 0)
---
