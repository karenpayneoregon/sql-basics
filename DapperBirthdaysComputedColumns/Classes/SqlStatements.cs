namespace DapperBirthdaysComputedColumns.Classes;
internal class SqlStatements
{
    public static string GetBirthdays =>
        """
        SELECT Id
            ,FirstName
            ,LastName
            ,BirthDate
            ,YearsOld
        FROM BirthDaysDatabase.dbo.BirthDays
        """;
}
