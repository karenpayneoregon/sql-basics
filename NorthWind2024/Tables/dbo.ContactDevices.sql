CREATE TABLE [dbo].[ContactDevices]
(
[ContactDeviceId] [int] NOT NULL IDENTITY(1, 1),
[ContactId] [int] NULL,
[PhoneTypeIdentifier] [int] NULL,
[PhoneNumber] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactDevices] ADD CONSTRAINT [PK_ContactDevices] PRIMARY KEY CLUSTERED ([ContactDeviceId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ContactDevices_ContactId] ON [dbo].[ContactDevices] ([ContactId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ContactDevices_PhoneTypeIdentifier] ON [dbo].[ContactDevices] ([PhoneTypeIdentifier]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactDevices] ADD CONSTRAINT [FK_ContactDevices_Contacts1] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO
ALTER TABLE [dbo].[ContactDevices] ADD CONSTRAINT [FK_ContactDevices_PhoneType] FOREIGN KEY ([PhoneTypeIdentifier]) REFERENCES [dbo].[PhoneType] ([PhoneTypeIdenitfier])
GO
