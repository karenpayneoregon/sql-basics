namespace DapperSamples.Classes;

    internal class SqlStatements
    {
        public static string GetColumnNamesStatement =>
            """
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @TableName
            ORDER BY COLUMN_NAME
            """;

        /// <summary>
        /// Get longest and shortest column lengths
        /// </summary>
        /// <param name="tableName">Table name to work on</param>
        /// <param name="columnName">Column name to get lengths for</param>
        /// <returns>SQL SELECT to populate List&lt;<see cref="ColumnResult"/>&gt; </returns>
        public static string MinMaxForColumn(string tableName, string columnName) =>
            $"""
             SELECT Minimum.{columnName} AS Value,
                    Minimum.[Length]
             FROM
             (
                 SELECT TOP 1
                        t.{columnName},
                        LEN(t.{columnName}) Length
                 FROM dbo.{tableName} AS t
                 ORDER BY [Length] ASC,
                          t.{columnName} ASC
             ) Minimum
             UNION
             SELECT Maximum.{columnName},
                    Maximum.[Length]
             FROM
             (
                 SELECT TOP 1
                        t.{columnName},
                        LEN(t.{columnName}) [Length]
                 FROM dbo.{tableName} AS t
                 ORDER BY [Length] DESC,
                          t.{columnName} ASC
             ) Maximum;
             """;

        public static string MaxForColumn() => 
            """
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
            """;

    }
