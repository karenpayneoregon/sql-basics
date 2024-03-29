using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperGetDatabaseAndTableNamesApp.Classes;
internal class SqlStatements
{
    public const string ReadStatement =
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
}
