CREATE TABLE [dbo].[Contacts]
(
[ContactId] [int] NOT NULL IDENTITY(1, 1),
[FirstName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContactTypeIdentifier] [int] NULL,
[FullName] AS (([FirstName]+' ')+[LastName])
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ContactId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Contacts_ContactTypeIdentifier] ON [dbo].[Contacts] ([ContactTypeIdentifier]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT [FK_Contacts_ContactType] FOREIGN KEY ([ContactTypeIdentifier]) REFERENCES [dbo].[ContactType] ([ContactTypeIdentifier])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Id', 'SCHEMA', N'dbo', 'TABLE', N'Contacts', 'COLUMN', N'ContactId'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Contact Type Identifier', 'SCHEMA', N'dbo', 'TABLE', N'Contacts', 'COLUMN', N'ContactTypeIdentifier'
GO
EXEC sp_addextendedproperty N'MS_Description', N'First name', 'SCHEMA', N'dbo', 'TABLE', N'Contacts', 'COLUMN', N'FirstName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Full name', 'SCHEMA', N'dbo', 'TABLE', N'Contacts', 'COLUMN', N'FullName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Last name', 'SCHEMA', N'dbo', 'TABLE', N'Contacts', 'COLUMN', N'LastName'
GO
