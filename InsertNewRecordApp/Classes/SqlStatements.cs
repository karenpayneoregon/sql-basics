namespace InsertNewRecordApp.Classes;

/// <summary>
/// All SQL statements for the project except for reset code.
/// </summary>
public class SqlStatements
{
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

    public static string ReadPeople =>
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM dbo.Person;
        """;

    public static string Get => 
        """
        SELECT Id,
               FirstName,
               LastName,
               BirthDate
        FROM dbo.Person
        WHERE Id = @Id;
        """;

    public static string UpdatePerson => 
        """
        UPDATE dbo.Person
        SET FirstName = @FirstName,
            LastName = @LastName,
            BirthDate = @BirthDate
        WHERE Id = @Id;
        """;

    public static string RemovePerson =>
        """
        DELETE FROM dbo.Person
        WHERE Id = @Id;
        """;

    public static string CountOfPeople =>
        """
        SELECT COUNT(Id)
        FROM dbo.Person;
        """;
}