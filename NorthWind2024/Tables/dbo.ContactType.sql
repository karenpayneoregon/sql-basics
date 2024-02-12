CREATE TABLE [dbo].[ContactType]
(
[ContactTypeIdentifier] [int] NOT NULL IDENTITY(1, 1),
[ContactTitle] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactType] ADD CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED ([ContactTypeIdentifier]) ON [PRIMARY]
GO
