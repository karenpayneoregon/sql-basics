CREATE TABLE [dbo].[Categories]
(
[CategoryID] [int] NOT NULL IDENTITY(1, 1),
[CategoryName] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Picture] [image] NULL,
[Photo] [varbinary] (max) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'Primary key', 'SCHEMA', N'dbo', 'TABLE', N'Categories', 'COLUMN', N'CategoryID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Name of a category', 'SCHEMA', N'dbo', 'TABLE', N'Categories', 'COLUMN', N'CategoryName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Description of category', 'SCHEMA', N'dbo', 'TABLE', N'Categories', 'COLUMN', N'Description'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Image which represents a category', 'SCHEMA', N'dbo', 'TABLE', N'Categories', 'COLUMN', N'Picture'
GO
