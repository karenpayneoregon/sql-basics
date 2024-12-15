--- 
/*
For EF.JSON localDb
*/

SELECT FirstName,
       LastName,
       FORMAT(DateOfBirth, 'dd/MM/yyyy ') AS DateOfBirth,
       JSON_VALUE(Addresses, '$[0].Street') AS Street,
       JSON_VALUE(Addresses, '$[0].City') AS City,
       JSON_VALUE(Addresses, '$[0].Company') AS Company
FROM [EF.Json].dbo.Person;

SELECT FirstName,
       LastName,
       FORMAT(DateOfBirth, 'dd/MM/yyyy ') AS DateOfBirth,
       JSON_VALUE(Addresses, '$[1].Street') AS Street,
       JSON_VALUE(Addresses, '$[1].City') AS City,
       JSON_VALUE(Addresses, '$[1].Company') AS Company
FROM [EF.Json].dbo.Person;

DECLARE @Index AS NCHAR(1) = 1;
SELECT Id,
       FirstName,
       LastName,
       DateOfBirth,
       JSON_VALUE(Addresses, '$[' + @Index + '].Street') AS Street,
       JSON_VALUE(Addresses, '$[' + @Index + '].City') AS City,
       JSON_VALUE(Addresses, '$[' + @Index + '].Company') AS Company
FROM dbo.Person;


DECLARE
    @INDEX INT = 0;
    WHILE @INDEX < 2 BEGIN
    SELECT
        ID,
        FIRSTNAME,
        LASTNAME,
        DATEOFBIRTH,
        JSON_VALUE(ADDRESSES,
        '$[' + CAST(@INDEX AS NVARCHAR(1)) + '].Street') AS STREET,
        JSON_VALUE(ADDRESSES,
        '$[' + CAST(@INDEX AS NVARCHAR(1)) + '].City') AS CITY,
        JSON_VALUE(ADDRESSES,
        '$[' + CAST(@INDEX AS NVARCHAR(1)) + '].Company') AS COMPANY
    FROM
        DBO.PERSON
    WHERE
        LASTNAME = 'Payne';
    SET   @INDEX = @INDEX + 1;
END;

WITH PersonAddresses
  AS (SELECT p.Id,
             p.FirstName,
             p.LastName,
             p.DateOfBirth,
             a.Street,
             a.City,
             a.Company,
             ROW_NUMBER() OVER (PARTITION BY p.Id ORDER BY a.Street) AS AddressIndex
        FROM dbo.Person p
       CROSS APPLY
             OPENJSON(p.Addresses)
             WITH (Street NVARCHAR(MAX),
                   City NVARCHAR(MAX),
                   Company NVARCHAR(MAX)) a
       WHERE p.LastName = @LastName)
SELECT pa.Id,
       pa.FirstName,
       pa.LastName,
       pa.DateOfBirth,
       pa.Street,
       pa.City,
       pa.Company
  FROM PersonAddresses pa;


USE [EF.JSON]
GO
 /****** Object:  Table [dbo].[Person]    Script Date: 12/11/2024 4:29:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DBO].[PERSON](
 [ID] [INT] IDENTITY(1, 1) NOT NULL,
 [FIRSTNAME] [NVARCHAR](MAX) NOT NULL,
 [LASTNAME] [NVARCHAR](MAX) NOT NULL,
 [DATEOFBIRTH] [DATETIME] NOT NULL,
 [ADDRESSES] [NVARCHAR](MAX) NULL,
 CONSTRAINT [PK_PERSON] PRIMARY KEY CLUSTERED
(
 [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [DBO].[PERSON] ON
GO
INSERT [DBO].[PERSON] ([ID], [FIRSTNAME], [LASTNAME], [DATEOFBIRTH], [ADDRESSES]) VALUES (1, N'Karen', N'Payne', CAST(N'1956-09-24T00:00:00.000' AS DATETIME), N'[{"City":"Ambler","Company":"Company1","Street":"123 Apple St"},{"City":"Portland","Company":"Company2","Street":"999 34th St"}]')
GO
INSERT [DBO].[PERSON] ([ID], [FIRSTNAME], [LASTNAME], [DATEOFBIRTH], [ADDRESSES]) VALUES (2, N'Bill', N'Jones', CAST(N'1960-04-24T00:00:00.000' AS DATETIME), N'[{"City":"Salem","Company":"ABC","Street":"34 Pine St"},{"City":"Portland","Company":"CDF","Street":"987 24th St"}]')
GO
SET IDENTITY_INSERT [DBO].[PERSON] OFF
GO
USE [MASTER]
GO
ALTER DATABASE [EF.JSON] SET READ_WRITE
GO