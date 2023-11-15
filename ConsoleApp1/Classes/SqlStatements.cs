namespace ConsoleApp1.Classes;
internal class SqlStatements
{
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
}
