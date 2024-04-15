using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace SqlServerLibrary.Classes;
public class StoredProcedureHelpers
{
    private readonly IDbConnection _cn = new SqlConnection(ConnectionString());

    public string FileName()
    {
        SqlConnectionStringBuilder builder = new(ConnectionString());
        return Path.Combine("StoredProcedures", $"{builder.InitialCatalog}_StoredProcedures.sql");
    }

    public string InitialCatalog()
    {
        SqlConnectionStringBuilder builder = new(ConnectionString());
        
        return $"{builder.DataSource}\\{builder.InitialCatalog}";
    }

    /// <summary>
    /// Read names of stored procedures from database in connection string
    /// </summary>
    /// <returns>List of Stored procedures if any</returns>
    public List<string> GetStoredProcedureName()
    {
        var statement =
            """
            SELECT name AS "ProcedureName"
             FROM sys.sysobjects
            WHERE type = 'P'
              AND LEFT(name, 3) NOT IN ( 'sp_', 'xp_', 'ms_' )
            ORDER BY name;
            """;

        List<string> storedProcedureNames = [];


        return _cn.Query<string>(statement).AsList();
    }

    /// <summary>
    /// Get the definition of a stored procedure
    /// </summary>
    /// <param name="procedureName">Name of stored procedure</param>
    /// <returns>Definition of stored procedure</returns>
    public string? GetStoredProcedureDefinition(string procedureName)
    {
        var statement =
            """
            SELECT      c.text
             FROM      sys.syscomments c
            INNER JOIN sys.sysobjects o
               ON o.id = c.id
            WHERE      o.type = 'P'
              AND      o.name = @ProcedureName;
            """;

        return _cn.Query<string>(statement, new { ProcedureName = procedureName}).FirstOrDefault();
    }

    public string? GetStoredProcedureDefinition1(string procedureName)
    {
        var statement =
            """
            DECLARE @Lines TABLE (Line NVARCHAR(MAX)) ;
            DECLARE @FullText NVARCHAR(MAX) = '' ;
            
            INSERT @Lines EXEC sp_helptext @ProcedureName ;
            SELECT @FullText = @FullText + Line FROM @Lines ; 
            
            SELECT @FullText ;
            """;

        return _cn.Query<string>(statement, new { ProcedureName = procedureName }).FirstOrDefault();
    }

}
