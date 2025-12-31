namespace SqlServerOutputToOtherTableExample.Classes;
/// <summary>
/// Provides SQL statements for inserting data into the database, including support for 
/// capturing inserted data and outputting it to other tables.
/// </summary>
/// <remarks>
/// Stored procedures could be used instead of inline SQL statements.
/// </remarks>
internal class SqlStatements
{
    public static string InsertPersonToTransactions =>
        """
        INSERT INTO dbo.Person (FirstName,LastName,Gender)
        OUTPUT Inserted.Id, SYSTEM_USER INTO dbo.Transactions(PersonId,UserName)
        VALUES (@FirstName, @LastName, @Gender);
        """;

    public static string InsertPersonToTransactionsWithAction =>
        """
        INSERT INTO dbo.Person (FirstName,LastName,Gender)
        OUTPUT Inserted.Id,SYSTEM_USER,'Insert'
        INTO dbo.Transactions (PersonId,
                               UserName,
        					   [Action])
        VALUES (@FirstName, @LastName, @Gender);
        """;

    public static string InsertPerson =>
        """
        INSERT INTO dbo.Person (FirstName,LastName,Gender)
        OUTPUT Inserted.Id
        VALUES (@FirstName, @LastName, @Gender);
        """;
}
