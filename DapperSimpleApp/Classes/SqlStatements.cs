namespace DapperSimpleApp.Classes
{
    /// <summary>
    /// Collection of SQL statements, feel free to move to stored procedures.
    /// </summary>
    /// <remarks>
    /// If at some point this code is ported to NET8+, refactor strings to raw string literals 
    /// </remarks>
    internal class SqlStatements
    {
        /// <summary>
        /// Read all people from database
        /// </summary>
        public static string GetAllPeople = "SELECT Id,FirstName,LastName,BirthDate FROM dbo.Person;";
        /// <summary>
        /// Read a person by primary key
        /// </summary>
        public static string GetPerson    = "SELECT Id,FirstName,LastName,BirthDate FROM dbo.Person WHERE Id = @Id";

        /// <summary>
        /// Update a person by primary key
        /// </summary>
        public static string UpdatePerson = 
            "UPDATE [dbo].[Person] SET [FirstName] = @FirstName,[LastName] = @LastName,[BirthDate] = @BirthDate WHERE Id = @Id";

        /// <summary>
        /// Remove a person by primary key
        /// </summary>
        public static string RemovePerson = "DELETE FROM dbo.Person WHERE Id = @Id;";

        /// <summary>
        /// Insert a new person, return the new primary key
        /// </summary>
        public static string InsertPerson => 
            "INSERT INTO dbo.Person (FirstName,LastName,BirthDate) " +
            "VALUES (@FirstName, @LastName, @BirthDate);" +
            "SELECT CAST(scope_identity() AS int);";

        /// <summary>
        /// Insert a new person, return the new primary key
        /// </summary>
        public static string InsertPerson1 =
            "INSERT INTO dbo.Person (FirstName,LastName,BirthDate) " +
            "OUTPUT INSERTED.Id " +
            "VALUES (@FirstName, @LastName, @BirthDate)";

        /// <summary>
        /// For resetting Person table to default data. Also demonstrates and
        /// easy way to add multiple records at once. Note that Dapper does
        /// a foreach internally to perform the add operation and is not a bulk
        /// insert operation so use this for small additions only.
        /// </summary>
        public static string ResetTable =
            "INSERT INTO dbo.Person ([FirstName], [LastName], [BirthDate]) " +
            "VALUES " +
            "( N'Benny', N'Anderson', N'2005-05-27' ), " +
            "( N'Teri', N'Schaefer', N'2002-12-19' ), " +
            "( N'Clint', N'Mante', N'2005-09-15' ), " +
            "( N'Drew', N'Green', N'2002-01-08' ), " +
            "( N'Denise', N'Schaden', N'2001-01-08' )";
    }
}
