USE [master]
GO
/****** Object:  Database [DataGridViewCodeSample]    Script Date: 11/16/2023 5:14:04 AM ******/
CREATE DATABASE [DataGridViewCodeSample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataGridViewCodeSample', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DataGridViewCodeSample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DataGridViewCodeSample_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DataGridViewCodeSample_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DataGridViewCodeSample] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataGridViewCodeSample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataGridViewCodeSample] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataGridViewCodeSample] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataGridViewCodeSample] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DataGridViewCodeSample] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataGridViewCodeSample] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DataGridViewCodeSample] SET  MULTI_USER 
GO
ALTER DATABASE [DataGridViewCodeSample] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataGridViewCodeSample] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataGridViewCodeSample] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataGridViewCodeSample] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DataGridViewCodeSample] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DataGridViewCodeSample] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DataGridViewCodeSample] SET QUERY_STORE = OFF
GO
USE [DataGridViewCodeSample]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 11/16/2023 5:14:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[ColorId] [int] IDENTITY(1,1) NOT NULL,
	[ColorText] [nvarchar](max) NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/16/2023 5:14:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/16/2023 5:14:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Item] [nvarchar](max) NULL,
	[ColorId] [int] NULL,
	[CustomerId] [int] NULL,
	[Qty] [int] NULL,
	[InCart] [bit] NULL,
	[VendorId] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 11/16/2023 5:14:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Colors] ON 

INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (1, N'Red')
INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (2, N'Blue')
INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (3, N'Green')
INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (4, N'White')
INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (5, N'Brown')
INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (6, N'DarkOrchid')
INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (7, N'Black')
INSERT [dbo].[Colors] ([ColorId], [ColorText]) VALUES (8, N'Silver')
SET IDENTITY_INSERT [dbo].[Colors] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [FirstName], [LastName]) VALUES (1, N'Karen', N'Payne')
INSERT [dbo].[Customer] ([CustomerId], [FirstName], [LastName]) VALUES (2, N'Mary', N'Jones')
INSERT [dbo].[Customer] ([CustomerId], [FirstName], [LastName]) VALUES (3, N'Mike', N'Adams')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([id], [Item], [ColorId], [CustomerId], [Qty], [InCart], [VendorId]) VALUES (1, N'iPhone 11', 7, 1, 4, 1, 1)
INSERT [dbo].[Product] ([id], [Item], [ColorId], [CustomerId], [Qty], [InCart], [VendorId]) VALUES (2, N'iPhone 7', 7, 1, 1, 1, 3)
INSERT [dbo].[Product] ([id], [Item], [ColorId], [CustomerId], [Qty], [InCart], [VendorId]) VALUES (3, N'Galaxy Note', 4, 2, 3, 1, 2)
INSERT [dbo].[Product] ([id], [Item], [ColorId], [CustomerId], [Qty], [InCart], [VendorId]) VALUES (4, N'Galaxy S', 8, 2, 2, NULL, 4)
INSERT [dbo].[Product] ([id], [Item], [ColorId], [CustomerId], [Qty], [InCart], [VendorId]) VALUES (5, N'iPhone 6', 1, 2, 1, 1, 1)
INSERT [dbo].[Product] ([id], [Item], [ColorId], [CustomerId], [Qty], [InCart], [VendorId]) VALUES (6, N'iPhone 7', 2, 3, 1, 1, 4)
INSERT [dbo].[Product] ([id], [Item], [ColorId], [CustomerId], [Qty], [InCart], [VendorId]) VALUES (7, N'iPhone X', 8, 2, 3, NULL, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Vendors] ON 

INSERT [dbo].[Vendors] ([VendorId], [VendorName]) VALUES (1, N'Amazon')
INSERT [dbo].[Vendors] ([VendorId], [VendorName]) VALUES (2, N'eBay')
INSERT [dbo].[Vendors] ([VendorId], [VendorName]) VALUES (3, N'Best Buy')
INSERT [dbo].[Vendors] ([VendorId], [VendorName]) VALUES (4, N'Select')
SET IDENTITY_INSERT [dbo].[Vendors] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Colors] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([ColorId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Colors]
GO
USE [master]
GO
ALTER DATABASE [DataGridViewCodeSample] SET  READ_WRITE 
GO
