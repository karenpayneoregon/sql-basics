--- For NorthWind2024 database
--- These are the original statements which were placed into C# code.

DECLARE @TableNames TABLE (TableName NVARCHAR(128) NOT NULL);
INSERT INTO @TableNames (TableName)
VALUES ('Categories'),
       ('ContactDevices'),
       ('Countries'),
       ('ContactType'),
       ('PhoneType');

SELECT      TableSchema = s.name,
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
   AND      t.name IN ( SELECT TableName FROM @TableNames )
 GROUP BY t.name,
          s.name,
          p.rows
 ORDER BY s.name,
          t.name;

SELECT      t.name AS TableName,
            p.rows AS [RowCount]
  FROM      sys.tables t
 INNER JOIN sys.partitions p
    ON t.object_id = p.object_id
 WHERE      p.index_id IN ( 0, 1 )
 ORDER BY t.name;

SELECT      t.name AS TableName,
            p.rows AS [RowCount]
  FROM      sys.tables t
 INNER JOIN sys.partitions p
    ON t.object_id = p.object_id
 WHERE      p.index_id IN ( 0, 1 )
 ORDER BY p.rows DESC,
          t.name;


                SELECT 
                    [Schema] = s.name,
                    Name = t.name,
                    [RowCount] = p.rows
                FROM sys.tables t
                INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                INNER JOIN sys.indexes i ON t.object_id = i.object_id
                INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
                WHERE t.is_ms_shipped = 0
                GROUP BY t.name, s.name, p.rows
                ORDER BY s.name, t.name;