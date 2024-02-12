CREATE TABLE [dbo].[SupplierRegion]
(
[RegionId] [int] NOT NULL IDENTITY(1, 1),
[RegionDescription] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SupplierRegion] ADD CONSTRAINT [PK_SupplierRegion] PRIMARY KEY CLUSTERED ([RegionId]) ON [PRIMARY]
GO
