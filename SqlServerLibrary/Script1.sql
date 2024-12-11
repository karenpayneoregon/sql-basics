

SELECT T.name TableName,
       I.Rows NumberOfRows
  FROM sys.tables T
  JOIN sys.sysindexes I
    ON T.OBJECT_ID = I.ID
 WHERE indid IN ( 0, 1 )
 ORDER BY I.Rows DESC,
          T.name;


SELECT      t.name AS TableName,
            p.rows AS [RowCount]
  FROM      sys.tables t
 INNER JOIN sys.partitions p
    ON t.object_id = p.object_id
 WHERE      p.index_id IN ( 0, 1 )
 ORDER BY p.rows DESC,
          t.name;


