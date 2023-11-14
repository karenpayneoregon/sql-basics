--- 

    DECLARE @TableName sysname = 'Customers';
    DECLARE @SQL NVARCHAR(MAX);
    SELECT @SQL
        = STUFF(
    (
        SELECT '
    UNION ALL 
    select ' + QUOTENAME(C.TABLE_NAME, '''') + ' AS TableName, ' + QUOTENAME(C.COLUMN_NAME, '''')
               + ' AS ColumnName, MAX(LEN(LTRIM(' + QUOTENAME(C.COLUMN_NAME) + '))) as [MaxLength], '
               + QUOTENAME(C.DATA_TYPE, '''') + ' AS DataType, '
               + CAST(COALESCE(C.CHARACTER_MAXIMUM_LENGTH, C.NUMERIC_SCALE) AS VARCHAR(10)) + '  AS DataWidth FROM '
               + QUOTENAME(C.TABLE_NAME)
        FROM INFORMATION_SCHEMA.COLUMNS C
        WHERE C.TABLE_NAME = @TableName
              AND C.DATA_TYPE NOT LIKE '%text'
        FOR XML PATH(''), TYPE
    ).value('.', 'varchar(max)'),
    1   ,
    11  ,
    ''
               );
    EXECUTE (@SQL);