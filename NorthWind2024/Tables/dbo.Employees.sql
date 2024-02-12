CREATE TABLE [dbo].[Employees]
(
[EmployeeID] [int] NOT NULL IDENTITY(1, 1),
[LastName] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ContactTypeIdentifier] [int] NULL,
[TitleOfCourtesy] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BirthDate] [datetime] NULL,
[HireDate] [datetime] NULL,
[Address] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Region] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CountryIdentifier] [int] NULL,
[HomePhone] [nvarchar] (24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Extension] [nvarchar] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Notes] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ReportsTo] [int] NULL,
[ReportsToNavigationEmployeeID] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Employees_ContactTypeIdentifier] ON [dbo].[Employees] ([ContactTypeIdentifier]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Employees_CountryIdentifier] ON [dbo].[Employees] ([CountryIdentifier]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Employees_ReportsToNavigationEmployeeID] ON [dbo].[Employees] ([ReportsToNavigationEmployeeID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [FK_Employees_ContactType] FOREIGN KEY ([ContactTypeIdentifier]) REFERENCES [dbo].[ContactType] ([ContactTypeIdentifier])
GO
ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [FK_Employees_Countries] FOREIGN KEY ([CountryIdentifier]) REFERENCES [dbo].[Countries] ([CountryIdentifier])
GO
ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [FK_Employees_Employees_ReportsToNavigationEmployeeID] FOREIGN KEY ([ReportsToNavigationEmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
GO
