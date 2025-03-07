/*
	This version id starts at 1 rather than 15
	Alter several column definitions
*/

USE [ProductCatalog]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/2/2024 2:54:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Color] nvarchar(MAX) NULL,
	[Size] nvarchar(MAX) NULL,
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

INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (1, N'Adjustable Race', N'Magenta', N'62', 100.0000, 75, N'{"ManufacturingCost":14.672700,"Type":"Part","MadeIn":"US"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (2, N'Bearing Ball', N'Magenta', N'62', 15.9900, 90, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', N'["promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (3, N'BB Ball Bearing', N'Magenta', N'62', 28.9900, 80, N'{"ManufacturingCost":21.162700,"Type":"Part","MadeIn":"China"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (4, N'Blade', N'Magenta', N'62', 18.0000, 45, N'{}', N'["new"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (5, N'Sport-100 Helmet, Red', N'Red', N'72', 41.9900, 38, N'{"ManufacturingCost":30.652700,"Type":"Еquipment","MadeIn":"China"}', N'["promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (6, N'Sport-100 Helmet, Black', N'Black', N'72', 31.4900, 60, N'{"ManufacturingCost":18.672700,"Type":"Part","MadeIn":"US"}', N'["new","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (7, N'Mountain Bike Socks, M', N'White', N'M', 560.9900, 30, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', N'["sales","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (8, N'Mountain Bike Socks, L', N'White', N'L', 120.9900, 20, N'{"ManufacturingCost":88.322700,"Type":"Clothes"}', N'["sales","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (9, N'Long-Sleeve Logo Jersey, XL', N'Multi', N'XL', 44.9900, 60, N'{"ManufacturingCost":32.842700,"Type":"Clothes"}', N'["sales","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (10, N'Road-650 Black, 52', N'Black', N'52', 704.6900, 70, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (11, N'Mountain-100 Silver, 38', N'Silver', N'38', 359.9900, 45, N'{"ManufacturingCost":262.792700,"Type":"Bike","MadeIn":"UK"}', N'["promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (12, N'Road-250 Black, 48', N'Black', N'48', 299.0200, 25, N'{"ManufacturingCost":218.284600,"Type":"Bike","MadeIn":"UK"}', N'["new","promo"]')
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (13, N'ML Bottom Bracket', NULL, NULL, 101.2400, 50, N'{"ManufacturingCost":11.672700,"Type":"Part","MadeIn":"China"}', NULL)
INSERT [dbo].[Product] ([ProductID], [Name], [Color], [Size], [Price], [Quantity], [Data], [Tags]) VALUES (14, N'HL Bottom Bracket', NULL, NULL, 121.4900, 65, N'{"ManufacturingCost":88.687700,"Type":"Part","MadeIn":"China"}', NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
/****** Object:  StoredProcedure [dbo].[InsertProductFromJson]    Script Date: 6/2/2024 2:54:01 AM ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateProductFromJson]    Script Date: 6/2/2024 2:54:01 AM ******/
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
/****** Object:  StoredProcedure [dbo].[UpsertProductFromJson]    Script Date: 6/2/2024 2:54:01 AM ******/
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
