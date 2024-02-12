CREATE TABLE [dbo].[Orders]
(
[OrderID] [int] NOT NULL IDENTITY(1, 1),
[CustomerIdentifier] [int] NULL,
[EmployeeID] [int] NULL,
[OrderDate] [date] NOT NULL,
[RequiredDate] [date] NOT NULL,
[ShippedDate] [date] NOT NULL,
[DeliveredDate] [date] NOT NULL,
[ShipVia] [int] NULL,
[Freight] [money] NULL,
[ShipAddress] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ShipCity] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ShipRegion] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ShipPostalCode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ShipCountry] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Orders_CustomerIdentifier] ON [dbo].[Orders] ([CustomerIdentifier]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Orders_EmployeeID] ON [dbo].[Orders] ([EmployeeID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Orders_ShipVia] ON [dbo].[Orders] ([ShipVia]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_Orders_Customers2] FOREIGN KEY ([CustomerIdentifier]) REFERENCES [dbo].[Customers] ([CustomerIdentifier]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_Orders_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_Orders_Shippers] FOREIGN KEY ([ShipVia]) REFERENCES [dbo].[Shippers] ([ShipperID])
GO
