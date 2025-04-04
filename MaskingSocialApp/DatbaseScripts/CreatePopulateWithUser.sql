USE [ComputedSample4]
GO
/****** Object:  User [NonPrivilegedUser]    Script Date: 3/25/2025 3:44:18 AM ******/
CREATE USER [NonPrivilegedUser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Taxpayer]    Script Date: 3/25/2025 3:44:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Taxpayer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[SocialSecurityNumber] [nchar](9) MASKED WITH (FUNCTION = 'partial(0, "XXXXX", 4)') NULL,
	[PhoneNumber] [nchar](12) MASKED WITH (FUNCTION = 'partial(0, "XXXXX", 4)') NULL,
	[BirthDay] [date] MASKED WITH (FUNCTION = 'default()') NULL,
	[BirthYear]  AS (datepart(year,[BirthDay])),
 CONSTRAINT [PK_Taxpayer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Taxpayer] ON 

INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (1, N'Stefanie', N'Buckley', N'681123358', N'555-234-1111', CAST(N'1984-08-07' AS Date))
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (2, N'Sandy', N'Mc Gee', N'129872540', N'555-222-2222', CAST(N'2000-12-09' AS Date))
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (3, N'Lee', N'Warren', N'455425068', N'555-333-3333', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (4, N'Regina', N'Forbes', N'486584435', N'555-444-4444', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (5, N'Daniel', N'Kim', N'670071721', N'555-555-5555', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (6, N'Dennis', N'Nunez', N'758233136', N'555-666-6666', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (7, N'Myra', N'Zuniga', N'766850017', N'555-777-7777', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (8, N'Teddy', N'Ingram', N'244962791', N'555-888-8888', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (9, N'Annie', N'Larson', N'952712007', N'555-999-9999', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (10, N'Herman', N'Anderson', N'193605818', N'555-000-1111', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (11, N'Jenifer', N'Livingston', N'933641567', N'555-122-1222', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (12, N'Stefanie', N'Perez', N'343851791', N'555-222-1233', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (13, N'Chastity', N'Garcia', N'355358642', N'555-333-4567', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (14, N'Evelyn', N'Stokes', N'875854198', N'555-132-1579', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (15, N'Jeannie', N'Daniel', N'687479231', N'555-956-9165', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (16, N'Rickey', N'Santos', N'957589100', N'555-159-4627', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (17, N'Bobbie', N'Hurst', N'903164731', N'555-464-3333', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (18, N'Lesley', N'Lawson', N'784334646', N'555-890-1234', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (19, N'Shawna', N'Browning', N'889286763', N'555-125-5836', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (20, N'Theresa', N'Ross', N'398565322', N'555-770-1537', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (21, N'Tasha', N'Hughes', N'476855493', N'555-044-1555', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (22, N'Karla', N'Hale', N'751341505', N'555-055-7768', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (23, N'Otis', N'Holt', N'469015528', N'555-577-1124', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (24, N'Alisa', N'Browning', N'217183657', N'555-170-1234', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (25, N'Peggy', N'Donaldson', N'552413114', N'555-469-5467', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (26, N'Lisa', N'Bentley', N'644075665', N'555-102-6867', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (27, N'Vicky', N'Wiley', N'759119894', N'555-234-1377', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (28, N'Nicolas', N'Spence', N'900140755', N'555-247-4454', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (29, N'Miranda', N'Barnes', N'363728001', N'555-568-9986', NULL)
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber], [PhoneNumber], [BirthDay]) VALUES (30, N'Kara', N'Barry', N'231446003', N'555-301-9999', NULL)
SET IDENTITY_INSERT [dbo].[Taxpayer] OFF
GO
USE [master]
GO
ALTER DATABASE [ComputedSample4] SET  READ_WRITE 
GO
