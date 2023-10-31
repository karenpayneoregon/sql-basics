namespace InsertNewRecordApp.Classes;

/// <summary>
/// All SQL statements for the project except for reset code.
/// </summary>
public class SqlStatements
{
    /// <summary>
    /// Add new person, return new primary key
    /// </summary>
    public static string InsertPeople =>
        """
        INSERT INTO dbo.Person
        (
            FirstName,
            LastName,
            BirthDate
        )
        VALUES
        (@FirstName, @LastName, @BirthDate);
        SELECT CAST(scope_identity() AS int);
        """;

    /// <summary>
    /// Get all records from Person table
    /// </summary>
    public static string ReadPeople =>
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM dbo.Person;
        """;

    /// <summary>
    /// Get a single person by primary key
    /// </summary>
    public static string Get => 
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM dbo.Person
        WHERE Id = @Id;
        """;

    /// <summary>
    /// Update person by primary key
    /// </summary>
    public static string UpdatePerson => 
        """
        UPDATE dbo.Person
        SET FirstName = @FirstName,
            LastName = @LastName,
            BirthDate = @BirthDate
        WHERE Id = @Id;
        """;

    /// <summary>
    /// Remove person by primary key
    /// </summary>
    public static string RemovePerson =>
        """
        DELETE FROM dbo.Person
        WHERE Id = @Id;
        """;

    /// <summary>
    /// Get count of records for Person table
    /// </summary>
    public static string CountOfPeople =>
        """
        SELECT COUNT(Id)
        FROM dbo.Person;
        """;

    /// <summary>
    /// SELECT WHERE BETWEEN years for birth date
    /// </summary>
    public static string BirthDateBetweenYears => 
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM InsertExamples.dbo.Person
        WHERE YEAR(BirthDate)
        BETWEEN @StartYear AND @EndYear;
        """;
}