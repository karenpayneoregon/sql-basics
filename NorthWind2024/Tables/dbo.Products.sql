CREATE TABLE [dbo].[Products]
(
[ProductID] [int] NOT NULL IDENTITY(1, 1),
[ProductName] [nvarchar] (40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SupplierID] [int] NULL,
[CategoryID] [int] NULL,
[QuantityPerUnit] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UnitPrice] [money] NULL,
[UnitsInStock] [smallint] NULL,
[UnitsOnOrder] [smallint] NULL,
[ReorderLevel] [smallint] NULL,
[Discontinued] [bit] NOT NULL,
[DiscontinuedDate] [datetime2] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Products_CategoryID] ON [dbo].[Products] ([CategoryID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Products_SupplierID] ON [dbo].[Products] ([SupplierID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
