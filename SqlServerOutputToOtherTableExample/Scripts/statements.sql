INSERT INTO dbo.Person (FirstName,
                        LastName,
                        Gender)
OUTPUT Inserted.Id
VALUES ('Karen', 'Payne', 'Female');

-----------------------------------------------
INSERT INTO dbo.Person (FirstName,
                        LastName,
                        Gender)
OUTPUT Inserted.Id,
       SYSTEM_USER
INTO dbo.Transactions (PersonId,
                       UserName)
VALUES ('Karen', 'Payne', 'Female');
