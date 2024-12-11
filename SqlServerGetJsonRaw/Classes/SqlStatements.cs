namespace SqlServerGetJsonRaw.Classes;
internal class SqlStatements
{
    /// <summary>
    /// Gets the SQL query string that retrieves person addresses from the database.
    /// </summary>
    /// <remarks>   
    /// The query iterates through a JSON array of address  es for each person, extracting
    /// specific fields such as Street, City, and Company. It uses the <c>JSON_VALUE</c>
    /// function to parse JSON data and assumes the presence of a variable <c>@index</c>
    /// for iteration. The query filters results where the last name is 'Payne'.
    /// </remarks>
    /// <returns>
    /// A SQL query string that retrieves person addresses.
    /// </returns>
    public static string GetPersonAddresses =>
        """
        DECLARE @index INT = 0;
        WHILE @index < 2
        BEGIN
            SELECT Id,
                   FirstName,
                   LastName,
                   DateOfBirth,
                   JSON_VALUE(Addresses, '$[' + CAST(@index as nvarchar(1)) + '].Street') AS Street,
                   JSON_VALUE(Addresses, '$[' + CAST(@index as nvarchar(1)) + '].City')   AS City,
                   JSON_VALUE(Addresses, '$[' + CAST(@index as nvarchar(1)) + '].Company') AS Company
            FROM dbo.Person WHERE LastName = @LastName;
            SET @index = @index + 1;
        END;
        """;
}
