namespace FactoryConnectionSample.Classes
{
    public class SqlStatements
    {
        /// <summary>
        /// Gets the SQL query string for retrieving records from the <c>dbo.BirthDays</c> table.
        /// </summary>
        /// <remarks>
        /// The query selects the following columns:
        /// <list type="bullet">
        /// <item><description><c>Id</c> - The unique identifier of the record.</description></item>
        /// <item><description><c>FirstName</c> - The first name of the individual.</description></item>
        /// <item><description><c>LastName</c> - The last name of the individual.</description></item>
        /// <item><description><c>BirthDate</c> - The birthdate of the individual.</description></item>
        /// <item><description><c>Age</c> - The calculated age of the individual based on the current date.</description></item>
        /// </list>
        /// </remarks>
        public static string Read =>
            """
            SELECT Id,
                 FirstName,
                 LastName,
                 BirthDate,
                 (CAST(FORMAT(GETDATE(), 'yyyyMMdd') AS INTEGER) - CAST(FORMAT(BirthDate, 'yyyyMMdd') AS INTEGER)) / 10000 as Age
            FROM dbo.BirthDays;
            """;
    }
}
