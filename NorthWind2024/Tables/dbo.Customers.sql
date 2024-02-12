CREATE TABLE [dbo].[Customers]
(
[CustomerIdentifier] [int] NOT NULL IDENTITY(1, 1),
[CompanyName] [nvarchar] (40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ContactId] [int] NULL,
[Street] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Region] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CountryIdentifier] [int] NULL,
[Phone] [nvarchar] (24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fax] [nvarchar] (24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContactTypeIdentifier] [int] NULL,
[ModifiedDate] [datetime2] NULL CONSTRAINT [DF__Customers__Modif__4316F928] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers] ADD CONSTRAINT [PK_Customers_1] PRIMARY KEY CLUSTERED ([CustomerIdentifier]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [City] ON [dbo].[Customers] ([City]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [CompanyName] ON [dbo].[Customers] ([CompanyName]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Customers_ContactId] ON [dbo].[Customers] ([ContactId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Customers_ContactTypeIdentifier] ON [dbo].[Customers] ([ContactTypeIdentifier]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Customers_CountryIdentifier] ON [dbo].[Customers] ([CountryIdentifier]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [PostalCode] ON [dbo].[Customers] ([PostalCode]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Region] ON [dbo].[Customers] ([Region]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers] ADD CONSTRAINT [FK_Customers_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO
ALTER TABLE [dbo].[Customers] ADD CONSTRAINT [FK_Customers_ContactType] FOREIGN KEY ([ContactTypeIdentifier]) REFERENCES [dbo].[ContactType] ([ContactTypeIdentifier])
GO
ALTER TABLE [dbo].[Customers] ADD CONSTRAINT [FK_Customers_Countries] FOREIGN KEY ([CountryIdentifier]) REFERENCES [dbo].[Countries] ([CountryIdentifier])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Company', 'SCHEMA', N'dbo', 'TABLE', N'Customers', 'COLUMN', N'CompanyName'
GO
