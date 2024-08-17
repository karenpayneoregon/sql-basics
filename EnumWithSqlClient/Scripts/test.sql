/*
    This SQL script retrieves information about tables in a database.
    It selects the schema name, table name, and number of rows for each table.
    The script joins multiple system tables to gather the required information.
    It also applies filters to exclude system tables and tables with no rows.
    The result is grouped by table name, schema name, and number of rows.
    The final result is ordered by table name.
*/
SELECT 
    s.name AS SchemaName,
    t.name AS TableName,
    p.Rows
FROM 
    sys.tables t
INNER JOIN      
    sys.indexes i ON t.object_id = i.object_id
INNER JOIN 
    sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
INNER JOIN 
    sys.allocation_units a ON p.partition_id = a.container_id
LEFT OUTER JOIN 
    sys.schemas s ON t.schema_id = s.schema_id
WHERE 
    t.name NOT LIKE 'dt%' 
    AND t.is_ms_shipped = 0
    AND i.object_id > 255 
	AND p.rows > 0
GROUP BY 
    t.name, s.name, p.rows
ORDER BY 
     t.name