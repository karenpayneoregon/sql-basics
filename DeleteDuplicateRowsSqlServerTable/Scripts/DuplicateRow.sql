--- SQLEXPRESS Example database
SELECT Id,
       FirstName,
       LastName,
       BirthDay
FROM Examples.dbo.PersonWithDuplicates;

INSERT INTO dbo.PersonWithDuplicates
(
    FirstName,
    LastName,
    BirthDay
)
SELECT FirstName,
       LastName,
       BirthDay
FROM dbo.PersonWithDuplicates
WHERE Id = 2;

SELECT Id,
       FirstName,
       LastName,
       BirthDay
FROM Examples.dbo.PersonWithDuplicates;