using SqlServer.Library.Models;

namespace SqlServer.Library.Classes;
internal class SqlStatements
{
    /// <summary>
    /// Get all views for a database into a list of <see cref="ViewContainer"/>
    /// </summary>
    public const string GetViewsStatement = 
        """
        SELECT 
          SCHEMA_NAME(schema_id) AS [Schema],
          [Name]
        FROM sys.views;
        """;

    /// <summary>
    /// Get all tables for a database and database name into a list of <see cref="DataContainer"/>
    /// </summary>
    public const string GetDatabasesStatement =
        """
        DECLARE @sql NVARCHAR(MAX);
        
        SELECT @sql
            = (   SELECT ' UNION ALL
                SELECT ' +    +QUOTENAME(name, '''')
                         + ' as DatabaseName,
                       s.name COLLATE DATABASE_DEFAULT
                            AS SchemaName,
                       t.name COLLATE DATABASE_DEFAULT as TableName 
                       FROM ' + QUOTENAME(name) + '.sys.tables t
                       JOIN ' + QUOTENAME(name) + '.sys.schemas s
                            on s.schema_id = t.schema_id'
                    FROM sys.databases
                   WHERE state = 0
                   ORDER BY [name]
                  FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)');
        
        SET @sql
            = STUFF(@sql, 1, 12, '')
              + N' order by DatabaseName, 
                                                       SchemaName,
                                                       TableName';
        
        EXECUTE (@sql);
        """;

    /// <summary>
    /// Get column details for table, calling code must provide value for @TableName parameter
    /// </summary>
    public const string GetColumnDetailsForTable =
        """
        SELECT X.COLUMN_NAME AS ColumnName,
              X.ORDINAL_POSITION AS Position,
              X.TABLE_CATALOG AS Catalog,
              X.TABLE_SCHEMA AS TableSchema,
              X.COLUMN_DEFAULT AS ColumnDefault,
              X.DATA_TYPE AS DataType
        FROM INFORMATION_SCHEMA.COLUMNS AS X
        WHERE X.TABLE_NAME = @TableName 
        """;
}
