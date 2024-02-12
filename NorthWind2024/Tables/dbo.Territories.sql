CREATE TABLE [dbo].[Territories]
(
[TerritoryID] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TerritoryDescription] [nchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RegionID] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Territories] ADD CONSTRAINT [PK_Territories] PRIMARY KEY NONCLUSTERED ([TerritoryID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Territories_RegionID] ON [dbo].[Territories] ([RegionID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Territories] ADD CONSTRAINT [FK_Territories_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([RegionID])
GO
