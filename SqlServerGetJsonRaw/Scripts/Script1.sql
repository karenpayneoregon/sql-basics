
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


--- after changing from Company to AddressType

--- Create a database named EF.Json under localDb.
USE [EF.Json]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 12/21/2024 4:27:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Addresses] [nvarchar](max) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (1, N'Karen', N'Payne', CAST(N'1956-09-24T00:00:00.000' AS DateTime), N'[{"City":"Ambler","AddressType":"Home","Street":"123 Apple St"},{"City":"Portland","AddressType":"Shipto","Street":"999 34th St"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (2, N'Bill', N'Jones', CAST(N'1960-04-24T00:00:00.000' AS DateTime), N'[{"City":"Salem","AddressType":"Home","Street":"34 Pine St"},{"City":"Portland","AddressType":"Shipto","Street":"987 24th St"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (3, N'John', N'Gallager', CAST(N'1980-01-01T00:00:00.000' AS DateTime), N'[{"Street":"123 Main St","City":"Any town","AddressType":"Home"},{"Street":"456 Elm St","City":"Other town","AddressType":"Shipto"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (4, N'Mary', N'Lebow', CAST(N'1980-01-01T00:00:00.000' AS DateTime), N'[{"Street":"123 21 St","City":"Wyndmoor","AddressType":"Home"},{"Street":"123 21 St","City":"Wyndmoor","AddressType":"Shipto"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (5, N'Lana', N'Waelchi', CAST(N'2004-01-30T17:30:00.000' AS DateTime), N'[{"Street":"Brenna Roads","City":"North Eveland","AddressType":"Home"},{"Street":"Feeney Knoll","City":"Port Willyberg","AddressType":"Shipto"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (6, N'Ruben', N'McLaughlin', CAST(N'1985-09-17T06:34:01.947' AS DateTime), N'[{"Street":"Berneice Valleys","City":"Kerlukemouth","AddressType":"Home"},{"Street":"Wuckert Alley","City":"Howellstad","AddressType":"Shipto"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (7, N'Pablo', N'Goyette', CAST(N'2006-07-17T23:36:58.160' AS DateTime), N'[{"Street":"Austen Brook","City":"Jocelynmouth","AddressType":"Home"},{"Street":"Gibson Shore","City":"North Bernadine","AddressType":"Shipto"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (8, N'Lucille', N'Larkin', CAST(N'1993-02-23T07:21:32.990' AS DateTime), N'[{"Street":"Yesenia Corner","City":"Celestinoburgh","AddressType":"Home"},{"Street":"Melba Extensions","City":"Howellshire","AddressType":"Shipto"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (9, N'Marion', N'Kessler', CAST(N'1984-08-15T08:52:00.467' AS DateTime), N'[{"Street":"Kurt Harbors","City":"North Davinshire","AddressType":"Home"},{"Street":"Frankie Village","City":"Zachariahbury","AddressType":"Shipto"}]')
GO
INSERT [dbo].[Person] ([ID], [FirstName], [LastName], [DateOfBirth], [Addresses]) VALUES (10, N'Angel', N'Tillman', CAST(N'1993-11-15T23:18:15.183' AS DateTime), N'[{"Street":"Rutherford Gateway","City":"Kossstad","AddressType":"Home"},{"Street":"Mekhi Brooks","City":"Welchshire","AddressType":"Shipto"}]')
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
USE [master]
GO
ALTER DATABASE [EF.Json] SET  READ_WRITE 
GO
