CREATE TABLE [dbo].[OrderDetails]
(
[OrderID] [int] NOT NULL,
[ProductID] [int] NOT NULL,
[UnitPrice] [money] NOT NULL,
[Quantity] [smallint] NOT NULL,
[Discount] [real] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails] ADD CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED ([OrderID], [ProductID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OrderDetails_ProductID] ON [dbo].[OrderDetails] ([ProductID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails] ADD CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] ADD CONSTRAINT [FK_Order_Details_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID])
GO
