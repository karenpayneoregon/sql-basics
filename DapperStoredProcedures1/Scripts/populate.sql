USE [DapperStoredProcedures]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 6/20/2024 12:51:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[GenderId] [int] NULL,
	[BirthDate] [date] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenderType]    Script Date: 6/20/2024 12:51:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenderType](
	[GenderId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_GenderType] PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (1, N'Stefanie', N'Buckley', 1, CAST(N'2000-02-02' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (2, N'Sandy', N'Mc Gee', 1, CAST(N'1978-12-23' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (3, N'Lee', N'Warren', 2, CAST(N'1966-03-13' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (4, N'Regina', N'Forbes', 1, CAST(N'1945-02-22' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (5, N'Daniel', N'Kim', 2, CAST(N'1999-06-12' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (6, N'Dennis', N'Nunez', 2, CAST(N'1984-11-11' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (7, N'Myra', N'Zuniga', 1, CAST(N'1989-07-23' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (8, N'Teddy', N'Ingram', 2, CAST(N'1955-03-04' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (9, N'Annie', N'Larson', 1, CAST(N'2002-12-12' AS Date))
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [GenderId], [BirthDate]) VALUES (10, N'Herman', N'Anderson', 3, CAST(N'1987-02-23' AS Date))
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[GenderType] ON 

INSERT [dbo].[GenderType] ([GenderId], [Description]) VALUES (1, N'Female')
INSERT [dbo].[GenderType] ([GenderId], [Description]) VALUES (2, N'Male')
INSERT [dbo].[GenderType] ([GenderId], [Description]) VALUES (3, N'Other')
SET IDENTITY_INSERT [dbo].[GenderType] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_GenderType] FOREIGN KEY([GenderId])
REFERENCES [dbo].[GenderType] ([GenderId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_GenderType]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllEmployees]    Script Date: 6/20/2024 12:51:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetAllEmployees] AS
BEGIN
SELECT      E.Id,
            E.FirstName,
            E.LastName,
            E.GenderId,
            GT.[Description] AS Gender,
			E.BirthDate
  FROM      dbo.Employee AS E
 INNER JOIN dbo.GenderType AS GT
    ON E.GenderId = GT.GenderId;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetEmployeeByGender]    Script Date: 6/20/2024 12:51:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetEmployeeByGender] (@GenderId AS INT) AS
BEGIN
SELECT Id,
       FirstName,
       LastName,
       GenderId,
       BirthDate
  FROM DapperStoredProcedures.dbo.Employee
 WHERE GenderId = @GenderId;
END

GO
USE [master]
GO
ALTER DATABASE [DapperStoredProcedures] SET  READ_WRITE 
GO
