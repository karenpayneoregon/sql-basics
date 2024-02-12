CREATE TABLE [dbo].[Countries]
(
[CountryIdentifier] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Countries] ADD CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryIdentifier]) ON [PRIMARY]
GO
