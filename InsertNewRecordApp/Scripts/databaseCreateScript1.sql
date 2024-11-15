USE [master]
GO
/****** Object:  Database [InsertExamples]    Script Date: 11/1/2023 1:51:26 AM ******/
CREATE DATABASE [InsertExamples]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InsertExamples', FILENAME = N'C:\Users\paynek\InsertExamples.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InsertExamples_log', FILENAME = N'C:\Users\paynek\InsertExamples_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [InsertExamples] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InsertExamples].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InsertExamples] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InsertExamples] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InsertExamples] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InsertExamples] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InsertExamples] SET ARITHABORT OFF 
GO
ALTER DATABASE [InsertExamples] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InsertExamples] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InsertExamples] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InsertExamples] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InsertExamples] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InsertExamples] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InsertExamples] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InsertExamples] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InsertExamples] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InsertExamples] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InsertExamples] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InsertExamples] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InsertExamples] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InsertExamples] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InsertExamples] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InsertExamples] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InsertExamples] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InsertExamples] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InsertExamples] SET  MULTI_USER 
GO
ALTER DATABASE [InsertExamples] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InsertExamples] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InsertExamples] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InsertExamples] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InsertExamples] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InsertExamples] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [InsertExamples] SET QUERY_STORE = OFF
GO
USE [InsertExamples]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/1/2023 1:51:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 11/1/2023 1:51:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[BirthDate] [date] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (1, N'Stefanie', N'Buckley', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (2, N'Sandy', N'Mc Gee', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (3, N'Lee', N'Warren', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (4, N'Regina', N'Forbes', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (5, N'Daniel', N'Kim', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (6, N'Dennis', N'Nunez', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (7, N'Myra', N'Zuniga', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (8, N'Teddy', N'Ingram', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (9, N'Annie', N'Larson', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (10, N'Herman', N'Anderson', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (11, N'Jenifer', N'Livingston', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (12, N'Stefanie', N'Perez', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (13, N'Chastity', N'Garcia', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (14, N'Evelyn', N'Stokes', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (15, N'Jeannie', N'Daniel', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (16, N'Rickey', N'Santos', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (17, N'Bobbie', N'Hurst', 1)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (18, N'Lesley', N'Lawson', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (19, N'Shawna', N'Browning', 0)
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Active]) VALUES (20, N'Theresa', N'Ross', 1)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (1, N'Anne', N'Marks', CAST(N'2009-06-14' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (2, N'Christian', N'Marks', CAST(N'2008-06-14' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (3, N'Ada', N'Stroman', CAST(N'2009-07-06' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (4, N'Christian', N'Pfeffer', CAST(N'2008-06-11' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (5, N'Jimmy', N'Wiegand', CAST(N'2006-05-18' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (6, N'Beulah', N'Beahan', CAST(N'2007-03-26' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (7, N'Lamar', N'Pfeffer', CAST(N'2000-10-20' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (8, N'Beverly', N'Swift', CAST(N'2004-12-10' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (9, N'Denise', N'Littel', CAST(N'1999-05-25' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (10, N'Tracy', N'Johnson', CAST(N'2009-10-01' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (11, N'Chad', N'Rodriguez', CAST(N'2007-06-15' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (12, N'Amy', N'Welch', CAST(N'2000-03-30' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (13, N'Ted', N'Thiel', CAST(N'1999-04-04' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (14, N'Luis', N'Lockman', CAST(N'2007-10-15' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (15, N'Natalie', N'Hintz', CAST(N'2003-07-21' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (16, N'Bessie', N'Frami', CAST(N'2005-11-16' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (17, N'Cecilia', N'Rau', CAST(N'2004-10-23' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (18, N'Rex', N'Mills', CAST(N'2003-01-31' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (19, N'Gerald', N'Zieme', CAST(N'2004-03-11' AS Date))
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [BirthDate]) VALUES (20, N'Stacey', N'Runte', CAST(N'2002-08-03' AS Date))
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
USE [master]
GO
ALTER DATABASE [InsertExamples] SET  READ_WRITE 
GO
