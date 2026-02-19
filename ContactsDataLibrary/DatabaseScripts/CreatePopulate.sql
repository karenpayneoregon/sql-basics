USE master;
/* ============================================================
   Contacts Database - Normalized Schema (SQL Server)
   - Reference tables use explicit primary key values in INSERTs
   ============================================================ */

IF DB_ID(N'Contacts') IS NULL
BEGIN
    CREATE DATABASE Contacts;
END
GO

USE Contacts;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ============================================================
   Drop tables in FK-safe order
   ============================================================ */
IF OBJECT_ID(N'dbo.PersonDevice', N'U') IS NOT NULL DROP TABLE dbo.PersonDevice;
IF OBJECT_ID(N'dbo.PersonAddress', N'U') IS NOT NULL DROP TABLE dbo.PersonAddress;
IF OBJECT_ID(N'dbo.Device', N'U') IS NOT NULL DROP TABLE dbo.Device;
IF OBJECT_ID(N'dbo.Address', N'U') IS NOT NULL DROP TABLE dbo.Address;
IF OBJECT_ID(N'dbo.Person', N'U') IS NOT NULL DROP TABLE dbo.Person;

IF OBJECT_ID(N'dbo.DeviceType', N'U') IS NOT NULL DROP TABLE dbo.DeviceType;
IF OBJECT_ID(N'dbo.AddressType', N'U') IS NOT NULL DROP TABLE dbo.AddressType;
IF OBJECT_ID(N'dbo.StateProvince', N'U') IS NOT NULL DROP TABLE dbo.StateProvince;
IF OBJECT_ID(N'dbo.Country', N'U') IS NOT NULL DROP TABLE dbo.Country;
IF OBJECT_ID(N'dbo.Gender', N'U') IS NOT NULL DROP TABLE dbo.Gender;
GO

/* ============================================================
   Reference Tables
   ============================================================ */

-- Gender reference table
CREATE TABLE dbo.Gender
(
    GenderId      TINYINT NOT NULL CONSTRAINT PK_Gender PRIMARY KEY,
    GenderName    NVARCHAR(20) NOT NULL CONSTRAINT UQ_Gender_GenderName UNIQUE
);
GO

-- Country reference table (seed with United States)
CREATE TABLE dbo.Country
(
    CountryId     SMALLINT NOT NULL CONSTRAINT PK_Country PRIMARY KEY,
    CountryName   NVARCHAR(100) NOT NULL,
    CountryCode2  CHAR(2) NOT NULL CONSTRAINT UQ_Country_Code2 UNIQUE
);
GO

-- State/Province reference table
CREATE TABLE dbo.StateProvince
(
    StateProvinceId  SMALLINT NOT NULL CONSTRAINT PK_StateProvince PRIMARY KEY,
    CountryId        SMALLINT NOT NULL,
    StateCode        CHAR(2) NOT NULL,
    StateName        NVARCHAR(100) NOT NULL,

    CONSTRAINT FK_StateProvince_Country
        FOREIGN KEY (CountryId) REFERENCES dbo.Country(CountryId),

    CONSTRAINT UQ_StateProvince_Country_StateCode
        UNIQUE (CountryId, StateCode)
);
GO

-- Address Type reference table
CREATE TABLE dbo.AddressType
(
    AddressTypeId    TINYINT NOT NULL CONSTRAINT PK_AddressType PRIMARY KEY,
    AddressTypeName  NVARCHAR(30) NOT NULL CONSTRAINT UQ_AddressType_Name UNIQUE
);
GO

-- Device Type reference table (Home phone, Work phone, Home email, Work email)
CREATE TABLE dbo.DeviceType
(
    DeviceTypeId     TINYINT NOT NULL CONSTRAINT PK_DeviceType PRIMARY KEY,
    DeviceTypeName   NVARCHAR(30) NOT NULL CONSTRAINT UQ_DeviceType_Name UNIQUE,
    DeviceKind       NVARCHAR(10) NOT NULL, -- 'Phone' or 'Email'
    CONSTRAINT CK_DeviceType_DeviceKind CHECK (DeviceKind IN (N'Phone', N'Email'))
);
GO

/* ============================================================
   Core Tables
   ============================================================ */

-- Person table
CREATE TABLE dbo.Person
(
    PersonId      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Person PRIMARY KEY,
    FirstName     NVARCHAR(100) NOT NULL,
    LastName      NVARCHAR(100) NOT NULL,
    MiddleName    NVARCHAR(100) NULL,
    DateOfBirth   DATE NULL,
    GenderId      TINYINT NULL,
    Notes         NVARCHAR(2000) NULL,

    CreatedAt     DATETIME2(0) NOT NULL CONSTRAINT DF_Person_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt     DATETIME2(0) NOT NULL CONSTRAINT DF_Person_UpdatedAt DEFAULT (SYSUTCDATETIME()),
    RowVersion    ROWVERSION NOT NULL,

    CONSTRAINT FK_Person_Gender
        FOREIGN KEY (GenderId) REFERENCES dbo.Gender(GenderId)
);
GO

-- Address table
CREATE TABLE dbo.Address
(
    AddressId        INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Address PRIMARY KEY,
    AddressLine1     NVARCHAR(200) NOT NULL,
    AddressLine2     NVARCHAR(200) NULL,
    City             NVARCHAR(100) NOT NULL,
    StateProvinceId  SMALLINT NOT NULL,
    PostalCode       NVARCHAR(20) NOT NULL,

    CreatedAt        DATETIME2(0) NOT NULL CONSTRAINT DF_Address_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt        DATETIME2(0) NOT NULL CONSTRAINT DF_Address_UpdatedAt DEFAULT (SYSUTCDATETIME()),
    RowVersion       ROWVERSION NOT NULL,

    CONSTRAINT FK_Address_StateProvince
        FOREIGN KEY (StateProvinceId) REFERENCES dbo.StateProvince(StateProvinceId)
);
GO

-- Junction: PersonAddress
CREATE TABLE dbo.PersonAddress
(
    PersonAddressId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PersonAddress PRIMARY KEY,
    PersonId        INT NOT NULL,
    AddressId       INT NOT NULL,
    AddressTypeId   TINYINT NOT NULL,
    IsPrimary       BIT NOT NULL CONSTRAINT DF_PersonAddress_IsPrimary DEFAULT (0),
    StartDate       DATE NULL,
    EndDate         DATE NULL,

    CreatedAt       DATETIME2(0) NOT NULL CONSTRAINT DF_PersonAddress_CreatedAt DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT FK_PersonAddress_Person
        FOREIGN KEY (PersonId) REFERENCES dbo.Person(PersonId),

    CONSTRAINT FK_PersonAddress_Address
        FOREIGN KEY (AddressId) REFERENCES dbo.Address(AddressId),

    CONSTRAINT FK_PersonAddress_AddressType
        FOREIGN KEY (AddressTypeId) REFERENCES dbo.AddressType(AddressTypeId),

    CONSTRAINT CK_PersonAddress_Dates
        CHECK (EndDate IS NULL OR StartDate IS NULL OR EndDate >= StartDate)
);
GO

CREATE INDEX IX_PersonAddress_PersonId ON dbo.PersonAddress(PersonId);
CREATE INDEX IX_PersonAddress_AddressId ON dbo.PersonAddress(AddressId);
GO

-- Device table
CREATE TABLE dbo.Device
(
    DeviceId       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Device PRIMARY KEY,
    DeviceTypeId   TINYINT NOT NULL,
    DeviceValue    NVARCHAR(320) NOT NULL, -- emails fit; phones fit too
    Extension      NVARCHAR(10) NULL,
    IsActive       BIT NOT NULL CONSTRAINT DF_Device_IsActive DEFAULT (1),

    CreatedAt      DATETIME2(0) NOT NULL CONSTRAINT DF_Device_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt      DATETIME2(0) NOT NULL CONSTRAINT DF_Device_UpdatedAt DEFAULT (SYSUTCDATETIME()),
    RowVersion     ROWVERSION NOT NULL,

    CONSTRAINT FK_Device_DeviceType
        FOREIGN KEY (DeviceTypeId) REFERENCES dbo.DeviceType(DeviceTypeId)
);
GO

CREATE UNIQUE INDEX UX_Device_DeviceType_Value ON dbo.Device(DeviceTypeId, DeviceValue);
GO

-- Junction: PersonDevice
CREATE TABLE dbo.PersonDevice
(
    PersonDeviceId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PersonDevice PRIMARY KEY,
    PersonId       INT NOT NULL,
    DeviceId       INT NOT NULL,
    IsPrimary      BIT NOT NULL CONSTRAINT DF_PersonDevice_IsPrimary DEFAULT (0),
    StartDate      DATE NULL,
    EndDate        DATE NULL,

    CreatedAt      DATETIME2(0) NOT NULL CONSTRAINT DF_PersonDevice_CreatedAt DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT FK_PersonDevice_Person
        FOREIGN KEY (PersonId) REFERENCES dbo.Person(PersonId),

    CONSTRAINT FK_PersonDevice_Device
        FOREIGN KEY (DeviceId) REFERENCES dbo.Device(DeviceId),

    CONSTRAINT CK_PersonDevice_Dates
        CHECK (EndDate IS NULL OR StartDate IS NULL OR EndDate >= StartDate)
);
GO

CREATE INDEX IX_PersonDevice_PersonId ON dbo.PersonDevice(PersonId);
CREATE INDEX IX_PersonDevice_DeviceId ON dbo.PersonDevice(DeviceId);
GO

/* ============================================================
   Seed Data (explicit PK values for reference tables)
   ============================================================ */

-- Gender
INSERT INTO dbo.Gender (GenderId, GenderName)
VALUES
(1, N'Male'),
(2, N'Female'),
(3, N'Other');

-- Country (United States)
INSERT INTO dbo.Country (CountryId, CountryName, CountryCode2)
VALUES
(1, N'United States', 'US');

-- Address Types
INSERT INTO dbo.AddressType (AddressTypeId, AddressTypeName)
VALUES
(1, N'Home'),
(2, N'Work'),
(3, N'Mailing'),
(4, N'Other');

-- Device Types
INSERT INTO dbo.DeviceType (DeviceTypeId, DeviceTypeName, DeviceKind)
VALUES
(1, N'Home phone', N'Phone'),
(2, N'Work phone', N'Phone'),
(3, N'Home email', N'Email'),
(4, N'Work email', N'Email');

-- States (US only; CountryId = 1)
INSERT INTO dbo.StateProvince (StateProvinceId, CountryId, StateCode, StateName)
VALUES
( 1, 1, 'AL', 'Alabama'),
( 2, 1, 'AK', 'Alaska'),
( 3, 1, 'AZ', 'Arizona'),
( 4, 1, 'AR', 'Arkansas'),
( 5, 1, 'CA', 'California'),
( 6, 1, 'CO', 'Colorado'),
( 7, 1, 'CT', 'Connecticut'),
( 8, 1, 'DE', 'Delaware'),
( 9, 1, 'FL', 'Florida'),
(10, 1, 'GA', 'Georgia'),
(11, 1, 'HI', 'Hawaii'),
(12, 1, 'ID', 'Idaho'),
(13, 1, 'IL', 'Illinois'),
(14, 1, 'IN', 'Indiana'),
(15, 1, 'IA', 'Iowa'),
(16, 1, 'KS', 'Kansas'),
(17, 1, 'KY', 'Kentucky'),
(18, 1, 'LA', 'Louisiana'),
(19, 1, 'ME', 'Maine'),
(20, 1, 'MD', 'Maryland'),
(21, 1, 'MA', 'Massachusetts'),
(22, 1, 'MI', 'Michigan'),
(23, 1, 'MN', 'Minnesota'),
(24, 1, 'MS', 'Mississippi'),
(25, 1, 'MO', 'Missouri'),
(26, 1, 'MT', 'Montana'),
(27, 1, 'NE', 'Nebraska'),
(28, 1, 'NV', 'Nevada'),
(29, 1, 'NH', 'New Hampshire'),
(30, 1, 'NJ', 'New Jersey'),
(31, 1, 'NM', 'New Mexico'),
(32, 1, 'NY', 'New York'),
(33, 1, 'NC', 'North Carolina'),
(34, 1, 'ND', 'North Dakota'),
(35, 1, 'OH', 'Ohio'),
(36, 1, 'OK', 'Oklahoma'),
(37, 1, 'OR', 'Oregon'),
(38, 1, 'PA', 'Pennsylvania'),
(39, 1, 'RI', 'Rhode Island'),
(40, 1, 'SC', 'South Carolina'),
(41, 1, 'SD', 'South Dakota'),
(42, 1, 'TN', 'Tennessee'),
(43, 1, 'TX', 'Texas'),
(44, 1, 'UT', 'Utah'),
(45, 1, 'VT', 'Vermont'),
(46, 1, 'VA', 'Virginia'),
(47, 1, 'WA', 'Washington'),
(48, 1, 'WV', 'West Virginia'),
(49, 1, 'WI', 'Wisconsin'),
(50, 1, 'WY', 'Wyoming'),
(51, 1, 'DC', 'District of Columbia');
GO
