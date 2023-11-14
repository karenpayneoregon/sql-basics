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

}
