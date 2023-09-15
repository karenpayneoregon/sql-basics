--- 
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