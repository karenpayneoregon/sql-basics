namespace DeleteDuplicateRowsSqlServerTable.Classes;
internal class SqlStatements
{
    /// <summary>
    /// Gets the SQL statement used to delete duplicate rows from the <c>PersonWithDuplicates</c> table.
    /// </summary>
    /// <remarks>
    /// This SQL statement removes duplicate rows while retaining the row with the minimum <c>Id</c>.
    /// Duplicates are identified based on matching <c>FirstName</c>, <c>LastName</c>, and other relevant columns.
    /// </remarks>
    public static string DeleteStatement => 
        """
        DELETE FROM dbo.PersonWithDuplicates 
        WHERE Id IN (
            SELECT Id FROM dbo.PersonWithDuplicates 
            EXCEPT SELECT MIN(Id) FROM dbo.PersonWithDuplicates 
            GROUP BY FirstName, LastName
            );
        """;

    
    /// <summary>
    /// Gets the SQL statement for creating and populating a table 
    /// with duplicate records based on specific criteria.
    /// </summary>
    /// <remarks>
    /// This property defines a SQL script that:
    /// <list type="bullet">
    /// <item>Creates a table named <c>@TempTable</c>.</item>
    /// <item>Populates the table with sample data.</item>
    /// <item>Identifies duplicate records based on the <c>name</c> and <c>email</c> columns.</item>
    /// <item>Returns the duplicate records along with their IDs.</item>
    /// </list>
    /// The script is useful for testing and demonstrating duplicate detection logic.
    /// </remarks>
    /// <returns>
    /// A SQL script as a <see cref="string"/> that can be executed to identify duplicates.
    /// </returns>
    public static string CreatePopulateTableGetDuplicates =>
        """
        DECLARE @TempTable TABLE (id INT,name VARCHAR(10),email VARCHAR(50));
        
        INSERT @TempTable VALUES (1, 'John', 'John-email');
        INSERT @TempTable VALUES (2, 'John', 'John-email');
        INSERT @TempTable VALUES (3, 'Fred', 'John-email');
        INSERT @TempTable VALUES (4, 'Fred', 'fred-email');
        INSERT @TempTable VALUES (5, 'Sam', 'sam-email');
        INSERT @TempTable VALUES (6, 'Sam', 'sam-email');    
        
        SELECT y.id, y.name, y.email FROM @TempTable y
            INNER JOIN
            (
                SELECT name,email,COUNT(*) AS CountOf FROM @TempTable
                GROUP BY name,email HAVING COUNT(*) > 1
            ) dt ON y.name = dt.name AND y.email = dt.email;                   
        """;
}
