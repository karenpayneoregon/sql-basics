/****** 
	IMPORTANT
	Change path before executing 
******/

USE [master]
GO
/****** Object:  Database [ComputedSample4]    Script Date: 1/7/2024 4:35:20 AM ******/
CREATE DATABASE [ComputedSample4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ComputedSample4', FILENAME = N'C:\Users\paynek\ComputedSample4.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ComputedSample4_log', FILENAME = N'C:\Users\paynek\ComputedSample4_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ComputedSample4] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ComputedSample4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ComputedSample4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ComputedSample4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ComputedSample4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ComputedSample4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ComputedSample4] SET ARITHABORT OFF 
GO
ALTER DATABASE [ComputedSample4] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ComputedSample4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ComputedSample4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ComputedSample4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ComputedSample4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ComputedSample4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ComputedSample4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ComputedSample4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ComputedSample4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ComputedSample4] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ComputedSample4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ComputedSample4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ComputedSample4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ComputedSample4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ComputedSample4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ComputedSample4] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ComputedSample4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ComputedSample4] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ComputedSample4] SET  MULTI_USER 
GO
ALTER DATABASE [ComputedSample4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ComputedSample4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ComputedSample4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ComputedSample4] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ComputedSample4] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ComputedSample4] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ComputedSample4] SET QUERY_STORE = OFF
GO
USE [ComputedSample4]
GO
/****** Object:  Table [dbo].[Taxpayer]    Script Date: 1/7/2024 4:35:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Taxpayer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[SocialSecurityNumber] [nchar](9) MASKED WITH (FUNCTION = 'partial(0, "XXXXX", 4)') NULL,
 CONSTRAINT [PK_Taxpayer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Taxpayer] ON 
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (1, N'Stefanie', N'Buckley', N'681123358')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (2, N'Sandy', N'Mc Gee', N'129872540')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (3, N'Lee', N'Warren', N'455425068')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (4, N'Regina', N'Forbes', N'486584435')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (5, N'Daniel', N'Kim', N'670071721')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (6, N'Dennis', N'Nunez', N'758233136')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (7, N'Myra', N'Zuniga', N'766850017')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (8, N'Teddy', N'Ingram', N'244962791')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (9, N'Annie', N'Larson', N'952712007')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (10, N'Herman', N'Anderson', N'193605818')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (11, N'Jenifer', N'Livingston', N'933641567')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (12, N'Stefanie', N'Perez', N'343851791')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (13, N'Chastity', N'Garcia', N'355358642')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (14, N'Evelyn', N'Stokes', N'875854198')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (15, N'Jeannie', N'Daniel', N'687479231')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (16, N'Rickey', N'Santos', N'957589100')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (17, N'Bobbie', N'Hurst', N'903164731')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (18, N'Lesley', N'Lawson', N'784334646')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (19, N'Shawna', N'Browning', N'889286763')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (20, N'Theresa', N'Ross', N'398565322')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (21, N'Tasha', N'Hughes', N'476855493')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (22, N'Karla', N'Hale', N'751341505')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (23, N'Otis', N'Holt', N'469015528')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (24, N'Alisa', N'Browning', N'217183657')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (25, N'Peggy', N'Donaldson', N'552413114')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (26, N'Lisa', N'Bentley', N'644075665')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (27, N'Vicky', N'Wiley', N'759119894')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (28, N'Nicolas', N'Spence', N'900140755')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (29, N'Miranda', N'Barnes', N'363728001')
GO
INSERT [dbo].[Taxpayer] ([Id], [FirstName], [LastName], [SocialSecurityNumber]) VALUES (30, N'Kara', N'Barry', N'231446003')
GO
SET IDENTITY_INSERT [dbo].[Taxpayer] OFF
GO
USE [master]
GO
ALTER DATABASE [ComputedSample4] SET  READ_WRITE 
GO
