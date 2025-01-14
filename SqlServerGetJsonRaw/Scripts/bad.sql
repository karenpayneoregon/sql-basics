--- 
/*
For EF.JSON localDb before changing from Company to AddressType
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

---------------------------------------------------------------------------------------------------
USE [EF.Json]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 12/26/2022 3:13:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](max) NOT NULL,
	[ContactName] [nvarchar](max) NOT NULL,
	[MailSettings] [nvarchar](max) NOT NULL,
	[GeneralSettings] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 12/26/2022 3:13:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Addresses] [nvarchar](max) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [EF.Json] SET  READ_WRITE 
GO
