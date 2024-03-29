USE [master]
GO
/****** Object:  Database [ProductCatalog]    Script Date: 12/7/2023 3:49:07 AM ******/
CREATE DATABASE [ProductCatalog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProductCatalog', FILENAME = N'C:\Users\paynek\ProductCatalog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProductCatalog_log', FILENAME = N'C:\Users\paynek\ProductCatalog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ProductCatalog] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProductCatalog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProductCatalog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProductCatalog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProductCatalog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProductCatalog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProductCatalog] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProductCatalog] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProductCatalog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProductCatalog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProductCatalog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProductCatalog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProductCatalog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProductCatalog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProductCatalog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProductCatalog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProductCatalog] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProductCatalog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProductCatalog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProductCatalog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProductCatalog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProductCatalog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProductCatalog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProductCatalog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProductCatalog] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProductCatalog] SET  MULTI_USER 
GO
ALTER DATABASE [ProductCatalog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProductCatalog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProductCatalog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProductCatalog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProductCatalog] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProductCatalog] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProductCatalog] SET QUERY_STORE = OFF
GO
USE [ProductCatalog]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/7/2023 3:49:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Color] [nvarchar](15) NULL,
	[Size] [nvarchar](5) NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NULL,
	[Data] [nvarchar](4000) NULL,
	[Tags] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (15, N'Adjustable Race', N'Magenta', N'62', 100.0000, 75, N'{"ManufacturingCost":14.672700,"Type":"Part","MadeIn":"US"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (16, N'Bearing Ball', N'Magenta', N'62', 15.9900, 90, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', N'["promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (17, N'BB Ball Bearing', N'Magenta', N'62', 28.9900, 80, N'{"ManufacturingCost":21.162700,"Type":"Part","MadeIn":"China"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (18, N'Blade', N'Magenta', N'62', 18.0000, 45, N'{}', N'["new"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (19, N'Sport-100 Helmet, Red', N'Red', N'72', 41.9900, 38, N'{"ManufacturingCost":30.652700,"Type":"Еquipment","MadeIn":"China"}', N'["promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (20, N'Sport-100 Helmet, Black', N'Black', N'72', 31.4900, 60, N'{"ManufacturingCost":18.672700,"Type":"Part","MadeIn":"US"}', N'["new","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (21, N'Mountain Bike Socks, M', N'White', N'M', 560.9900, 30, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', N'["sales","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (22, N'Mountain Bike Socks, L', N'White', N'L', 120.9900, 20, N'{"ManufacturingCost":88.322700,"Type":"Clothes"}', N'["sales","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (23, N'Long-Sleeve Logo Jersey, XL', N'Multi', N'XL', 44.9900, 60, N'{"ManufacturingCost":32.842700,"Type":"Clothes"}', N'["sales","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (24, N'Road-650 Black, 52', N'Black', N'52', 704.6900, 70, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (25, N'Mountain-100 Silver, 38', N'Silver', N'38', 359.9900, 45, N'{"ManufacturingCost":262.792700,"Type":"Bike","MadeIn":"UK"}', N'["promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (26, N'Road-250 Black, 48', N'Black', N'48', 299.0200, 25, N'{"ManufacturingCost":218.284600,"Type":"Bike","MadeIn":"UK"}', N'["new","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (27, N'ML Bottom Bracket', NULL, NULL, 101.2400, 50, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (28, N'HL Bottom Bracket', NULL, NULL, 121.4900, 65, N'{"ManufacturingCost":88.687700,"Type":"Part","MadeIn":"China"}', NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
/****** Object:  StoredProcedure [dbo].[InsertProductFromJson]    Script Date: 12/7/2023 3:49:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertProductFromJson](@ProductJson NVARCHAR(MAX))
AS BEGIN

	INSERT INTO dbo.Product(Name,Color,Size,Price,Quantity,Data,Tags)
	OUTPUT  INSERTED.ProductID
	SELECT Name,Color,Size,Price,Quantity,Data,Tags
	FROM OPENJSON(@ProductJson)
		WITH (	Name nvarchar(100) N'strict $."Name"',
				Color nvarchar(30),
				Size nvarchar(10),
				Price money N'strict $."Price"',
				Quantity int,
				Data nvarchar(max) AS JSON,
				Tags nvarchar(max) AS JSON) as json
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductFromJson]    Script Date: 12/7/2023 3:49:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateProductFromJson](@ProductID int, @ProductJson NVARCHAR(MAX))
AS BEGIN

	UPDATE dbo.Product SET
		Name = json.Name,
		Color = json.Color,
		Size = json.Size,
		Price = json.Price,
		Quantity = json.Quantity,
		Data = ISNULL(json.Data, dbo.Product.Data),
		Tags = ISNULL(json.Tags,dbo.Product.Tags)
	FROM OPENJSON(@ProductJson)
		WITH (	Name nvarchar(100) N'strict $."Name"',
				Color nvarchar(30),
				Size nvarchar(10),
				Price money N'strict $."Price"',
				Quantity int,
				Data nvarchar(max) AS JSON,
				Tags nvarchar(max) AS JSON) as json
	WHERE dbo.Product.ProductID = @ProductID

END
GO
/****** Object:  StoredProcedure [dbo].[UpsertProductFromJson]    Script Date: 12/7/2023 3:49:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpsertProductFromJson](@ProductID int, @ProductJson NVARCHAR(MAX))
AS BEGIN

	MERGE INTO dbo.Product
	USING ( SELECT Name,Color,Size,Price,Quantity,Data,Tags
			FROM OPENJSON(@ProductJson)
				WITH (
					Name nvarchar(100) N'strict $."Name"',
					Color nvarchar(30),
					Size nvarchar(10),
					Price money N'strict $."Price"',
					Quantity int,
					Data nvarchar(max) AS JSON,
					Tags nvarchar(max) AS JSON)) as json
	ON (dbo.Product.ProductID = @ProductID)
	WHEN MATCHED THEN
		UPDATE SET
			Name = json.Name,
			Color = json.Color,
			Size = json.Size,
			Price = json.Price,
			Quantity = json.Quantity,
			Data = json.Data,
			Tags = json.Tags
	WHEN NOT MATCHED THEN
		INSERT (Name,Color,Size,Price,Quantity,Data,Tags)
		VALUES (json.Name,json.Color,json.Size,json.Price,json.Quantity,json.Data,json.Tags);
END
GO
USE [master]
GO
ALTER DATABASE [ProductCatalog] SET  READ_WRITE 
GO
