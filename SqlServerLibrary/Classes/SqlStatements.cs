namespace SqlServerLibrary.Classes
{
    /// <summary>
    /// A container for all SQL statements
    /// </summary>
    /// <remarks>
    /// All statements are created in SSMS and pasted here.
    /// As a rule of thumb it's better to write statements in SSMS, validate they work
    /// as expected as added them as done here.
    /// </remarks>
    public class SqlStatements
    {
        /// <summary>
        /// Provides column names from the description property for each column in a specified table name
        /// </summary>
        public static string DescriptionStatement =>
            """
            SELECT      col.COLUMN_NAME AS ColumnName,
                        col.ORDINAL_POSITION AS Position,
            			ISNULL(prop.value,'(none)') AS [Description]
              FROM      INFORMATION_SCHEMA.TABLES AS tbl
             INNER JOIN INFORMATION_SCHEMA.COLUMNS AS col
                ON col.TABLE_NAME = tbl.TABLE_NAME
             INNER JOIN sys.columns AS sc
                ON sc.object_id   = OBJECT_ID(tbl.TABLE_SCHEMA + '.' + tbl.TABLE_NAME)
               AND sc.name        = col.COLUMN_NAME
              LEFT JOIN sys.extended_properties prop
                ON prop.major_id  = sc.object_id
               AND prop.minor_id  = sc.column_id
               AND prop.name      = 'MS_Description'
             WHERE      tbl.TABLE_NAME = @TableName
             ORDER BY col.ORDINAL_POSITION;
            """;

        /// <summary>
        /// Get default values for each column for tables using a connection object
        /// </summary>
        public static string GetDefaultValuesInDatabase =>
            """
            SELECT SO.[name] AS "TableName",
                   SC.[name] AS "ColumnName",
                   SM.[text] AS "DefaultValue"
            FROM dbo.sysobjects SO
                INNER JOIN dbo.syscolumns SC
                    ON SO.id = SC.id
                LEFT JOIN dbo.syscomments SM
                    ON SC.cdefault = SM.id
            WHERE SO.xtype = 'U'
                  AND SO.[name] <> 'sysdiagrams'
                  AND SM.[text] IS NOT NULL
            ORDER BY SO.[name],
                     SC.colid;
            """;

        /// <summary>
        /// Get all database names from master
        /// </summary>
        public static string GetDatabaseNames =>
            """
            SELECT TableName = DB_NAME(s_mf.database_id)
            FROM sys.master_files s_mf
            WHERE s_mf.state = 0 -- ONLINE
                  AND HAS_DBACCESS(DB_NAME(s_mf.database_id)) = 1
                  AND DB_NAME(s_mf.database_id) NOT IN ( 'master', 'tempdb', 'model', 'msdb' )
                  AND DB_NAME(s_mf.database_id)NOT LIKE 'ReportServer%'
            GROUP BY s_mf.database_id
            ORDER BY 1;
            """;

        /// <summary>
        /// Get details for a table which requires a table name in the calling code
        /// </summary>
        public static string TableDetails =>
            """
            SELECT c.[name] 'ColumnName',
                   t.[name] 'DataType',
                   c.[max_length] 'MaxLength',
                   c.[precision] 'Precision',
                   c.scale 'Scale',
                   c.is_nullable 'IsNullable',
                   ISNULL(i.is_primary_key, 0) 'PrimaryKey'
            FROM sys.columns c
                INNER JOIN sys.types t
                    ON c.user_type_id = t.user_type_id
                LEFT OUTER JOIN sys.index_columns ic
                    ON ic.object_id = c.object_id
                       AND ic.column_id = c.column_id
                LEFT OUTER JOIN sys.indexes i
                    ON ic.object_id = i.object_id
                       AND ic.index_id = i.index_id
            WHERE c.object_id = OBJECT_ID(@TableName);
            """;

        /// <summary>
        /// Get all computed columns in a database using a valid connection object
        /// </summary>
        public static string ComputedColumnDefinitions =>
            """
            SELECT SCHEMA_NAME(o.schema_id) 'SchemaName',
                   c.name AS 'ColumnName',
                   OBJECT_NAME(c.object_id) AS 'TableName',
                   TYPE_NAME(c.user_type_id) AS 'DataType',
                   c.definition 'Definition'
            FROM sys.computed_columns c
                JOIN sys.objects o
                    ON o.object_id = c.object_id
            ORDER BY SchemaName,
                     TableName,
                     c.column_id;
            """;

        /// <summary>
        /// Determine if table exists in a data using a table name as a parameter
        /// </summary>
        public static string TableExists =>
            """
            SELECT CASE
                WHEN EXISTS
                     (
                         (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName)
                     ) THEN
                    1
                ELSE
                    0
            END;
            """;

        /// <summary>
        /// Get names of user stored procedures that start with usp or usp_
        /// </summary>
        public static string GetUserStoredProcedureNames =>
            """
            SELECT [name]
            FROM sys.procedures
            WHERE [name] LIKE 'usp%' OR [name] LIKE 'usp_%';
            """;

        public static string WhereInCustomers =>
            """
            SELECT C.CustomerIdentifier,
                   C.CompanyName,
                   C.Street,
                   C.City,
                   C.CountryIdentifier,
                   CO.Name AS CountryName,
                   C.Phone,
                   C.ContactId,
                   CT.FirstName,
                   CT.LastName
            FROM dbo.Customers AS C
                INNER JOIN dbo.Contacts AS CT
                    ON C.ContactId = CT.ContactId
                INNER JOIN dbo.Countries AS CO
                    ON C.CountryIdentifier = CO.CountryIdentifier
            WHERE C.CustomerIdentifier IN ({0})
            ORDER BY C.CompanyName
            """;

        /// <summary>
        /// Get all date time columns for tables in database
        /// </summary>
        public static string GetAllDateTimeColumnsInDatabase =>
            """
            SELECT SCHEMA_NAME(t.schema_id) + '.' + t.name AS [TableName],
                   c.column_id "ColumnId",
                   c.name AS "ColumnName",
                   TYPE_NAME(c.user_type_id) AS DataType,
                   c.scale AS "Scale"
            FROM sys.columns c
                JOIN sys.tables t
                    ON t.object_id = c.object_id
            WHERE TYPE_NAME(c.user_type_id) IN ( 'date', 'datetimeoffset', 'datetime2', 'smalldatetime', 'datetime', 'time' )
            ORDER BY [TableName],
                     c.column_id;
            """;

        /// <summary>
        /// Get details for database tables
        /// Table name, constraint name, primary key column name, foreign table, foreign key column update rule delete rule 
        /// </summary>
        public static string TableConstraintsForDatabase =>
            """
            SELECT
                PrimaryKeyTable = QUOTENAME(PK.CONSTRAINT_SCHEMA) + '.' + QUOTENAME(PK.TABLE_NAME),
                ConstraintName = C.CONSTRAINT_NAME,
                PrimaryKeyColumn = CCU.COLUMN_NAME,
                ForeignKeyTable = QUOTENAME(FK.CONSTRAINT_SCHEMA) + '.' + QUOTENAME(FK.TABLE_NAME),
                ForeignKeyColumn = CU.COLUMN_NAME,
                UpdateRule = C.UPDATE_RULE,
                DeleteRule = C.DELETE_RULE
            FROM
                INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C 
                INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON 
                    C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME AND
                    C.CONSTRAINT_CATALOG = FK.CONSTRAINT_CATALOG AND
                    C.CONSTRAINT_SCHEMA = FK.CONSTRAINT_SCHEMA
                INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON 
                    C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME AND
                    C.UNIQUE_CONSTRAINT_CATALOG = PK.CONSTRAINT_CATALOG AND
                    C.UNIQUE_CONSTRAINT_SCHEMA = PK.CONSTRAINT_SCHEMA
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON 
                    C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME AND
                    C.CONSTRAINT_CATALOG = CU.CONSTRAINT_CATALOG AND
                    C.CONSTRAINT_SCHEMA = CU.CONSTRAINT_SCHEMA
                INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU ON 
                    PK.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME AND
                    PK.CONSTRAINT_CATALOG = CCU.CONSTRAINT_CATALOG AND
                    PK.CONSTRAINT_SCHEMA = CCU.CONSTRAINT_SCHEMA
            WHERE
                FK.CONSTRAINT_TYPE = 'FOREIGN KEY'
            ORDER BY
                PK.TABLE_NAME, 
                FK.TABLE_NAME
            """;

        /// <summary>
        /// Select for Customers which requires parameters for CustomerIdentifier, PhoneType and ContactType
        /// </summary>
        public static string GetCustomers =>
            """
            SELECT Cust.CustomerIdentifier,
                   Cust.CompanyName,
                   Cust.City,
                   Cust.PostalCode,
                   C.ContactId,
                   CO.CountryIdentifier,
                   CO.Name AS Country,
                   Cust.Phone,
                   Devices.PhoneTypeIdentifier,
                   Devices.PhoneNumber,
                   Cust.ContactTypeIdentifier,
                   C.FirstName,
                   C.LastName,
                   CT.ContactTitle
            FROM dbo.Customers AS Cust
                INNER JOIN dbo.ContactType AS CT
                    ON Cust.ContactTypeIdentifier = CT.ContactTypeIdentifier
                INNER JOIN dbo.Countries AS CO
                    ON Cust.CountryIdentifier = CO.CountryIdentifier
                INNER JOIN dbo.Contacts AS C
                    ON Cust.ContactId = C.ContactId
                INNER JOIN dbo.ContactDevices AS Devices
                    ON C.ContactId = Devices.ContactId
            WHERE (
                      Cust.CustomerIdentifier = @CustomerIdentifier
                      AND Devices.PhoneTypeIdentifier = @PhoneType
            		  AND Cust.ContactTypeIdentifier = @ContactType
                  );
            """;

        public static string TableNamesForDatabase(string databaseName) => 
            $"""
             SELECT TABLE_NAME AS TableName
              FROM [{databaseName}].INFORMATION_SCHEMA.TABLES
             WHERE TABLE_TYPE = 'BASE TABLE'
               AND TABLE_NAME <> 'sysdiagrams'
             ORDER BY TABLE_NAME;
             """;

        public static string DatabaseTablesRowCount => 
            """
            SELECT     QUOTENAME(SCHEMA_NAME(item.schema_id)) + '.' + QUOTENAME(item.name) AS [Name],
                       SUM(parts.[rows]) AS [RowCount]
             FROM      sys.objects AS item
            INNER JOIN sys.partitions AS parts
               ON item.object_id = parts.object_id
            WHERE      item.[type]     = 'U'
              AND      item.is_ms_shipped = 0x0
              AND      parts.index_id      < 2 -- 0:Heap, 1:Clustered
              AND  item.[name] <> 'sysdiagrams'
            GROUP BY item.schema_id,
                     item.[name]
            ORDER BY [Name];
            """;

        public static string DatabaseTablesRowCount1 =>
            """
            SELECT     TableSchema = s.name,
                       Name = t.name,
                       [RowCount] = p.rows
             FROM      sys.tables t
            INNER JOIN sys.schemas s
               ON t.schema_id = s.schema_id
            INNER JOIN sys.indexes i
               ON t.object_id = i.object_id
            INNER JOIN sys.partitions p
               ON i.object_id = p.object_id
              AND i.index_id  = p.index_id
            WHERE      t.is_ms_shipped = 0
            GROUP BY t.name,
                     s.name,
                     p.rows
            ORDER BY s.name,
                     t.name;
            """;
    }
}

