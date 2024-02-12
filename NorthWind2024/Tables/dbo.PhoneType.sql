CREATE TABLE [dbo].[PhoneType]
(
[PhoneTypeIdenitfier] [int] NOT NULL IDENTITY(1, 1),
[PhoneTypeDescription] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PhoneType] ADD CONSTRAINT [PK_PhoneType] PRIMARY KEY CLUSTERED ([PhoneTypeIdenitfier]) ON [PRIMARY]
GO
